using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Xpo;

namespace API.WebApi.Core;

public sealed class ObjectSpaceProviderFactory : IObjectSpaceProviderFactory {
    readonly ISecurityStrategyBase security;
    readonly ITypesInfo typesInfo;
    readonly IXpoDataStoreProvider dataStoreProvider;

    readonly IServiceProvider serviceProvider;
    public ObjectSpaceProviderFactory(IServiceProvider serviceProvider, ISecurityStrategyBase security, ITypesInfo typesInfo, IXpoDataStoreProvider dataStoreProvider) {
        this.security = security;
        this.serviceProvider = serviceProvider;
        this.typesInfo = typesInfo;
        this.dataStoreProvider = dataStoreProvider;
    }

    public IEnumerable<IObjectSpaceProvider> CreateObjectSpaceProviders() {
        yield return new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)security, dataStoreProvider, true);
        yield return new NonPersistentObjectSpaceProvider(serviceProvider, typesInfo, null);
    }
}
