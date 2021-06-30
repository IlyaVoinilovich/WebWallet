using Automatonymous;

using System;
using System.Threading.Tasks;
using WebWal.Interface;

namespace WebWal.StateMachine
{
    public class WebWalletTransactionStateMachine : MassTransitStateMachine<WebWalletTransactionState>
    {
        /// <summary>
        /// Investment approved by the order management
        /// </summary>
        public State WebWalletApproved { get; private set; }

        /// <summary>
        /// Investment revoked by the order management
        /// </summary>
        public State WebWalletRevoked { get; private set; }

        /// <summary>
        /// New investment received event
        /// </summary>
        public Event<ITransactionCreate> TransactionCreate { get; private set; }


        /// <summary>
        /// Order management state machine
        /// </summary>
        /// <param name="orderManagement"></param>
        public WebWalletTransactionStateMachine(IWebWallet orderManagement)
        {

            State(() => WebWalletApproved);
            State(() => WebWalletRevoked);

            Event(() => TransactionCreate, x => x.CorrelateById(ctx => ctx.Message.Transaction.Id).SelectId(context => Guid.NewGuid()));

            Initially(
                When(TransactionCreate)
                    // Set received Investment to machine state
                    .Then(ctx => ctx.Instance.Transaction = ctx.Data.Transaction)
                    // Log
                    .IfElse(ctx => orderManagement.TransactionCreate(ctx.Instance.Transaction),
                        ctx => ctx.TransitionTo(WebWalletApproved),
                        ctx => ctx.TransitionTo(WebWalletRevoked)));

            DuringAny(
                // Investment approved by order management
                When(WebWalletApproved.Enter)
                    // Log
                    // Send approved message
                    .Then(ctx => ctx.Publish<WebWalletTransactionState, ICreateTransactionCommandWebWalletApproved>(new
                    {
                        Transaction = ctx.Instance.Transaction
                    }))
                    .Finalize(),

                // Investment revoked by order management
                When(WebWalletRevoked.Enter)
                    // Send revoked message
                    .Then(ctx => ctx.Publish<WebWalletTransactionState, ICreateTransactionCommandWebWalletRevoked>(new
                    {
                        Transaction = ctx.Instance.Transaction
                    }))
                    .Finalize());

            SetCompletedWhenFinalized();
        }
    }
}

