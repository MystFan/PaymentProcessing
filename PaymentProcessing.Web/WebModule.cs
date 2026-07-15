using Autofac;
using PaymentProcessing.Application;
using PaymentProcessing.Application.Commands.CreateAccountCommand;
using PaymentProcessing.Application.Commands.CreateCustomerCommand;
using PaymentProcessing.Application.Commands.TransferAmountCommand;
using PaymentProcessing.DataAccess;
using PaymentProcessing.DataAccess.Repositories;

namespace PaymentProcessing.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateCustomerCommandValidator>().As<IFluentValidator>().InstancePerLifetimeScope();
            builder.RegisterType<CreateAccountCommandValidator>().As<IFluentValidator>().InstancePerLifetimeScope();
            builder.RegisterType<TransferAmountCommandValidator>().As<IFluentValidator>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
