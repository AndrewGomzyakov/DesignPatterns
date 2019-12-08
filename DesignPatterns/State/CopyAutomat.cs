using System;
using System.Reflection.PortableExecutable;
using System.Xml;

namespace State
{
    public class CopyAutomat
    {
        public int Cost => 10;
        public int MoneyAmount { get; set; }
        public IState State { get; set; }
        
        public Guid DeviceId { get; set; }
        public Guid DocumentId { get; set; }

        public CopyAutomat()
        {
            MoneyAmount = 0;
            State = new InitState();
        }

        public void AddMoney(int money)
        {
            State.AddMoney(this, money);
        }

        public void SelectDevice(Guid deviceId)
        {
            State.SelectDevice(this, DeviceId);
        }
        public void SelectDocument(Guid documentId)
        {
            State.SelectDocument(this, documentId);
        }

        public void Print()
        {
            State.Print(this);
        }

        public int GetChange()
        {
            return State.GetChange(this);
        }
    }

    public interface IState
    {
        void AddMoney(CopyAutomat automat, int money);
        void SelectDevice(CopyAutomat automat, Guid deviceId);
        void SelectDocument(CopyAutomat automat, Guid documentId);
        void Print(CopyAutomat automat);
        int GetChange(CopyAutomat automat);
    }

    public abstract class StateBase : IState
    {
        public virtual void AddMoney(CopyAutomat automat, int money)
        {
            automat.MoneyAmount += money;
            if (automat.MoneyAmount > automat.Cost && automat.State is InitState)
                automat.State = new MoneyAddedState();
        }

        public abstract void SelectDevice(CopyAutomat automat, Guid deviceId);

        public abstract void SelectDocument(CopyAutomat automat, Guid documentId);

        public abstract void Print(CopyAutomat automat);

        public virtual int GetChange(CopyAutomat automat)
        {
            var change = automat.MoneyAmount;
            automat.MoneyAmount = 0;
            automat.State = new InitState();
            automat.DeviceId = Guid.Empty;
            automat.DocumentId = Guid.Empty;
            return change;
        }
    }

    public class InitState : StateBase
    {
        public override void SelectDevice(CopyAutomat automat, Guid deviceId)
        {
            Console.WriteLine("Недостаточно средств");
        }

        public override void SelectDocument(CopyAutomat automat, Guid documentId)
        {
            Console.WriteLine("Недостаточно средств");
        }

        public override void Print(CopyAutomat automat)
        {
            Console.WriteLine("Недостаточно средств");
        }
    }

    public class MoneyAddedState : StateBase
    {
        public override void SelectDevice(CopyAutomat automat, Guid deviceId)
        {
            automat.DeviceId = deviceId;
            automat.State = new DeviceSelectedState();
        }

        public override void SelectDocument(CopyAutomat automat, Guid documentId)
        {
            Console.WriteLine("Устройство не выбрано");
        }

        public override void Print(CopyAutomat automat)
        {
            Console.WriteLine("Устройство не выбрано");
        }
    }

    public class DeviceSelectedState : StateBase
    {
        public override void SelectDevice(CopyAutomat automat, Guid deviceId)
        {
            automat.DeviceId = deviceId;
        }

        public override void SelectDocument(CopyAutomat automat, Guid documentId)
        {
            automat.DocumentId = documentId;
            automat.State = new DocumentSelectedState();
        }

        public override void Print(CopyAutomat automat)
        {
            Console.WriteLine("Документ не выбран");
        }
    }

    public class DocumentSelectedState : StateBase
    {
        public override void SelectDevice(CopyAutomat automat, Guid deviceId)
        {
            automat.DocumentId = Guid.Empty;
            automat.DeviceId = deviceId;
            automat.State = new DeviceSelectedState();
        }

        public override void SelectDocument(CopyAutomat automat, Guid documentId)
        {
            automat.DocumentId = documentId;
        }

        public override void Print(CopyAutomat automat)
        {
            automat.MoneyAmount -= automat.Cost;
            //print
            if (automat.MoneyAmount > automat.Cost)
                automat.State = new MoneyAddedState();
            else
                automat.State = new InitState();
        }
    }
}