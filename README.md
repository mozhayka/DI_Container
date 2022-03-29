# DI контейнер
Для того, чтобы определить, что такое DI контейнер и зачем он нужен, определим сначала, что такое SOLID

S: Single Responsibility Principle (Принцип единственной ответственности) \
O: Open-Closed Principle (Принцип открытости-закрытости) \
L: Liskov Substitution Principle (Принцип подстановки Барбары Лисков) \
I: Interface Segregation Principle (Принцип разделения интерфейса) \
D: Dependency Inversion Principle (Принцип инверсии зависимостей) 

Более подробно о SOLID можно почитать [здесь](https://habr.com/ru/company/ruvds/blog/426413/) \
Из этого списка важен последний пункт, а именно Dependency Inversion Principle. 

### Принцип инверсии зависимостей

Объектом зависимости должна быть абстракция, а не что-то конкретное

1) Модули верхних уровней не должны зависеть от модулей нижних уровней. Оба типа модулей должны зависеть от абстракций
2) Абстракции не должны зависеть от деталей. Детали должны зависеть от абстракций

Посмотрим на такую картинку

![image](https://user-images.githubusercontent.com/54811112/160558661-1d587892-7fc4-4e90-ade3-ed0792afec5d.png)

Вершины на ней обозначают модули, а ребра - то, кто какой модуль использует у себя. Допустим, хотим поменять модуль Е. 
В этом случае что-то может сломаться в модулях В и С, а затем и в А. В итоге небольшие изменения в одном модуле могут привести к большим изменениям во всей программе. 
Решить проблему поможет инверсия зависимостей. А именно, в модулях В и С вместо использования Е напрямую, можно написать интерфейс, который покажет, какие именно функции Е нужны

![image](https://user-images.githubusercontent.com/54811112/160562348-137f95ea-b9e2-4ec7-9672-c6f4fb9d5602.png)

Тем самым перенаправили стрелки из Е, то есть инвертировали зависимость

### DI контейнер
Роль контейнера заключается в предоставлении приложению правильной реализации на основе предоставленной конфигурации.
То есть можно представить контейнер как словарь, где абстракция является ключом, а связанное значение каждого ключа является определением того, как создать эту конкретную реализацию.

Рассмотрим функционал на примере [Simple Injector](https://docs.simpleinjector.org/en/latest/using.html)

Основным типом является класс Container. Экземпляр Container используется для регистрации сопоставлений между каждой абстракцией (сервисом) и соответствующей ей реализацией (компонентом).

#### Регистрация происходит с помощью вызова перегрузки Register
```
var container = new SimpleInjector.Container();

// Registrations here
container.Register<ILogger, FileLogger>();

// Request instance
ILogger logger = container.GetInstance<ILogger>();
```

#### Simple Injector поддерживает два сценария извлечения экземпляров компонентов:
1) Получение объекта заданным типом
```
var repository = container.GetInstance<IUserRepository>();

// Alternatively, you can use the weakly typed version
var repository = (IUserRepository)container.GetInstance(typeof(IUserRepository));
```
2) Получение коллекции объектов по их типу
```
IEnumerable<ICommand> commands = container.GetAllInstances<ICommand>();

// Alternatively, you can use the weakly typed version
IEnumerable<object> commands = container.GetAllInstances(typeof(ICommand));
```
#### Еще несколько примеров перегрузки Register
1) Настройка одного экземпляра, созданного вручную (Singleton) для постоянного возврата:
```
// Configuration
container.RegisterInstance<IUserRepository>(new SqlUserRepository());

// Usage
IUserRepository repository = container.GetInstance<IUserRepository>();
```

2) Настройка одного экземпляра с помощью делегата:
```
// Configuration
container.Register<IUserRepository>(
    () => new SqlUserRepository("some constr"),
    Lifestyle.Singleton);

// Usage
IUserRepository repository = container.GetInstance<IUserRepository>();
```

3) Настройка автоматически созданного нового экземпляра для возврата:
```
// Configuration
container.Register<IHandler<MoveCustomerCommand>, MoveCustomerHandler>();

// Alternatively you can supply the transient Lifestyle with the same effect.
container.Register<IHandler<MoveCustomerCommand>, MoveCustomerHandler>(
    Lifestyle.Transient);

// Usage
var handler = container.GetInstance<IHandler<MoveCustomerCommand>>();
```

4) Авторегистрация/Пакетная регистрация:
```
// Configuration
Assembly[] assemblies = // determine list of assemblies to search in
container.Register(typeof(IHandler<>), assemblies);
```

#### Коллекции
Помимо создания сопоставлений один к одному между абстракцией и реализацией, Simple Injector позволяет регистрировать набор реализаций для данной абстракции. Затем эти реализации могут быть запрошены из контейнера в виде коллекции экземпляров. Simple Injector содержит специальные методы для регистрации и разрешения коллекций типов.
```
// Configuration
// Registering a list of instances that will be created by the container.
// Supplying a collection of types is the preferred way of registering collections.
container.Collection.Register<ILogger>(typeof(MailLogger), typeof(SqlLogger));

// Register a fixed list (these instances should be thread-safe).
container.Collection.Register<ILogger>(new MailLogger(), new SqlLogger());

// Using a collection from another subsystem
container.Collection.Register<ILogger>(Logger.Providers);

// Usage
IEnumerable<ILogger> loggers = container.GetAllInstances<ILogger>();
```
Можно добавлять элементы в коллекцию так же с помощью Collection.Append:
```
container.Register<ILogger, FileLogger>();

container.Collection.Append<ILogger, MailLogger>(Lifestyle.Singleton);
container.Collection.Append<ILogger, SqlLogger>();
container.Collection.AppendInstance<ILogger>(new FileLogger>());
```

Автоматическая регистрация коллекций
```
Assembly[] assemblies = // determine list of assemblies to search in
container.Collection.Register<ILogger>(assemblies);
```
