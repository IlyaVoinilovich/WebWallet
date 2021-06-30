using Automatonymous;
using System;
using WebWal.ModelsModels;

namespace WebWal.StateMachine
{
    public class WebWalletTransactionState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }

        public CreateTransactionCommand Transaction { get; set; }

        public State State { get; set; }

        public CompositeEventStatus Approvement { get; set; }
    }
}
