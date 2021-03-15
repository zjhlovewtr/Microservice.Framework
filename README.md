# Microservice.Framework
##    一、简介
###    微服务框架，以特性驱动的微服务框架,基于netcore3.1开发
###    该微服务框架主要解决微服务通信和微服务集群的问题，帮助大家快速落地微服务项目
###    特征：
###    1、中台调用组件，
###    2、断路器Polly组件，
###    3、注册中心组件，
###    4、负载均衡组件，
###    5、动态中台调用组件，
###    6、微服务客户端组件，
###    7、框架异常组件
##     二、框架组件关系图
![微服务框架图](https://user-images.githubusercontent.com/20025647/111111860-1349e600-859a-11eb-89c2-0c8d76a748e0.png)

##  三、如何使用
    依赖环境
    1、aspnetcore3.1
    2、consul
    使用步骤
    1、从github下载该源码
    2、然后引入到项目中即可
##  2.1 先进入微服务项目中，进入到startup.cs文件，引入Microservice.Framework框架代码

    public void ConfigureServices(IServiceCollection services)
        {
            ........         
            // 4、添加服务注册
            services.AddServiceRegistry(options => {
                options.ServiceId = Guid.NewGuid().ToString(); // 微服务实例Id
                options.ServiceName = "SeckillServices"; // 微服务名称
                options.ServiceAddress = "https://localhost:5004"; // 微服务地址
                options.HealthCheckAddress = "/HealthCheck"; // 健康检测地址
                
                options.RegistryAddress = "http://localhost:8500"; // consul注册地址
            });
            ........
        }


##  2.2 然后进入聚合项目中(调用微服务的项目),进入到startup.cs文件，引入Microservice.Framework框架代码

       public void ConfigureServices(IServiceCollection services)
        {
            ........
            // 1、注册服务发现
            services.AddMicroClient(options =>
            {
                options.AssmelyName = "RuanMou.Projects.SeckillAggregateServices"; // 聚合项目名称
                options.dynamicMiddlewareOptions = mo =>
                {
                    mo.serviceDiscoveryOptions = sdo =>
                    { sdo.DiscoveryAddress = "http://localhost:8500"; }; // consul服务发现地址
                };
            });
            .......
        }
        
        
        
##  2.3 最后在聚合项目中创建调用微服务项目的接口，引入特性MicroClient，PostPath
     
    /// <summary>
    /// 微服务客户端
    /// </summary>
    [MicroClient("https", "OrderServices")]
    public interface IOrderClient
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        [PostPath("/Orders ")]
        public Order CreateOrder(Order order);

        public Order CreateOrderItem();
    }
    
        特性介绍
        1、MicroClient：配置该接口哪个微服务
        https：被调用的微服务通信协议
        OrderServices：被调用的微服务名称
        2、PostPath：配置接口方法通过什么方式调用，支持Post,Get,Delete,Put
        /Orders 被调用微服务路径
 ##    2.4 启动微服务和聚合微服务项目即可
