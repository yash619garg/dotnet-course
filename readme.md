<!-- C# learning journey -->

<!-- ## 1. dotnet watch run

- `dotnet watch run` is a **.NET CLI command** that runs your app **with hot-reload / auto-restart** whenever you change code files.
- when you save changes → it **rebuilds + restarts** -->

<!-- ## 2. API

- Api’s allow us to interact with Database safely -->

<!-- ## 3. Models and one to many relationship

to understand these topic please refer to the api/Models/: Stocks(parent) and Comments(child)

-->

<!-- ## 4. ORM : Object Relational Mapping

    ORM is a technique/tool that helps you connect OOP classes (C# objects) with Relational Database tables automatically.
    So instead of writing SQL queries manually, you work with C# classes and objects, and ORM handles DB operations.
    Without ORM: You have to write SQL like:
    With ORM: You do everything using objects.

    Entity Framework Core (EF Core) is an ORM used in ASP.NET Core.
    It allows you to:
    Create models (classes)
    Map them to DB tables
    Query DB using LINQ
    Apply migrations automatically

    install extension NuGet Gallery and then 3 EF : 1.sqlserver 2.tools 3.Design (according to your .net version)
    after that dotnet restore in terminal

    ## for further information visit Api/Data/ApplicationDBContext.cs and then program.cs and readLine:
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    then i create database and add data string by gpt and change password and database name in appSettings
    Run: dotnet ef migrations add init
    --This command creates a Migration file based on the changes in your Models/DbContext.
    --It compares: Your current Models + DbContext with The last migration (or empty DB)

    Run : dotnet ef database update
    --This command applies migrations to the database

-->

<!-- ## 5. Controllers

        A Controller is a class in ASP.NET Core that:
        1. handles incoming HTTP requests (GET, POST, PUT, DELETE)
        2. processes them using business logic
        3. and returns a response (JSON / View / Status code)
        So it acts like a middleman between: Client (frontend/Postman/browser) and Your backend logic + database

        Why Controllers are used?
        Because they help you:
        ✅ organize your API routes
        ✅ keep code clean and structured
        ✅ separate concerns (MVC pattern)
        ✅ easily manage CRUD operations

        In Web API:
        Controllers mostly return JSON instead of Views.

-->

<!-- ## 6. DTO (Data Transfer Object)

        -- A DTO is a simple class used to transfer data between layers (API ↔ Client) without exposing the full Model/Entity.
        -- DTO is like a “data packet” for sending/receiving only required data.
        -- Because Entity/Model classes are not always safe or suitable to expose.
        -- DTO helps in:

        -- 1) Security
        -- You may have sensitive fields in Entity like: Password , Role ,Internal IDs
        -- DTO avoids sending them.

        -- 2) Control over response
        -- DTO allows you to decide:
        -- which fields to send
        -- which fields to accept

        -- 3) Clean API contract
        -- Your API response becomes stable even if database schema changes.

        -- 4) Avoid over-fetching / under-fetching
        -- Entity may have many columns, but frontend needs only few.
        -- DTO gives only what’s needed.

        -- Types of DTOs
        1) Request DTO (Input DTO)
        -- Used when client sends data to API
        -- Example: CreateStockDto
        -- Client sends required fields only.

        2) Response DTO (Output DTO)
        -- Used when API returns data
        -- Example: StockDto
        -- API sends selected fields only.
-->

<!-- ## 7. Mapper + Extension Method

        -- A Mapper is responsible for converting one object type into another object type.
        -- In your case: Stock (Entity/Model) → StockDto (DTO)

        -- Why do we need a Mapper?
        -- Because you should not write mapping logic again and again inside controllers.

        -- Without mapper:
        -- Controller becomes messy:
        -- many repeated lines of mapping

        -- With mapper:
        -- clean controller
        -- reusable mapping logic
        -- easy maintenance
        -- follows separation of concerns

        -- Extension Method
        -- An extension method allows you to add methods to an existing class without modifying its source code
-->

<!-- ## 8. Post Api

        -- POST is an HTTP method used to send data from client to server to:
        -- create a new resource (new record in DB)

        -- Why do we use POST?
        -- Because GET is only for reading data.
        -- POST is used when you want to insert/save something.
        -- Client sends data in request body (JSON).

        -- POST sends data in Request Body (JSON).
        -- So in ASP.NET Core:
        -- [FromBody] is used to receive that JSON into C# object.

        -- Typical API Flow of POST
        -- Client sends POST request with JSON body
        -- Controller receives it (model binding)
        -- Backend validates data
        -- ORM (EF Core) inserts into DB
        -- API returns response:
        -- Success: 201 Created (or 200 OK)
        -- Failure: 400 BadRequest

-->

<!-- ## 9. Put Api
        -- PUT is an HTTP method used to update an existing resource in the database.
        -- In simple words : PUT = “Update data”

        -- Use PUT when:
        -- you already have a record in DB
        -- you want to change its values

        -- PUT vs POST (very important)
        -- Method	Purpose
        -- POST	        Create new record
        -- PUT	        Update existing record

        -- How PUT request looks (theory)
        Usually: PUT /api/stock/{id}
        Example: PUT /api/stock/5

        -- Full Update vs Partial Update
        -- PUT = Full Update
        In REST theory, PUT means: Replace the full resource
        So ideally client sends all fields. the field not sent by treat as default values

        -- PATCH = Partial Update
        PATCH means: Update only some fields
        Example: only update purchase price.

        -- What happens internally during PUT?
        Client sends request with id
        API finds record in DB
        If record not found → return 404 Not Found
        If found → update properties
        Save changes
        Return response → usually 200 OK or 204 NoContent

 -->

<!-- ## 10. Delete Api

        -- DELETE is an HTTP method used to remove a resource (record) from the database.
        -- In simple words:DELETE = “Remove data”

        -- When do we use DELETE?
        -- a record already exists
        -- you want to remove it permanently

        -- Typical URL format : DELETE /api/stock/{id}

        -- What happens internally in EF Core (theory)
        -- Client sends request with id
        -- API searches record in DB
        -- If not found → return 404 Not Found
        -- If found → mark it for deletion
        -- SaveChanges() executes SQL DELETE
        -- Return response

 -->

<!-- ## 11. Async/Await

        -- async/await is a C# feature used to write non-blocking asynchronous code.
        -- Instead of waiting and blocking the thread, the program can continue doing other work while the task completes.

        -- Why do we need Async?
        -- Because some operations take time, like:
        ✅ Database calls (EF Core)
        ✅ API calls (HTTP requests)
        ✅ File read/write
        ✅ Network operations
        -- These are called I/O operations (Input/Output).

        -- What happens without async?
        thread gets blocked
        server can handle fewer requests
        slower performance under load

        -- What happens with async?
        ✅ server thread is free while waiting for DB
        ✅ better scalability
        ✅ handles more users concurrently
        ✅ faster API under traffic

        -- async keyword means:
        -- This method contains asynchronous operations.
        -- It allows using await inside the method.

        -- await keyword means:
        -- Wait for this task to finish WITHOUT blocking the thread.
        -- So it pauses method execution, but does not freeze the server thread.

        A thread is like a worker in a restaurant.
        Each incoming HTTP request needs a worker to handle it.
        ASP.NET Core has a limited number of workers (threads) available.
        If all workers are busy, new customers (requests) have to wait.

        Blocking means: The worker/thread is stuck waiting and cannot do anything else.

        By using await The method says: Okay, I’ll come back when DB finishes. Meanwhile, thread is free.”
        So the method execution pauses, but thread is released back to ASP.NET Core.
        ✅ thread becomes available to handle other incoming requests

        -- it is similar to the thread working in os

        -- Common mistakes in async/await

        ❌ 1) Using .Result or .Wait()
        This can cause:
        deadlocks
        blocking
        performance issues

        ❌ 2) Forgetting await
        If you forget await:
        operation may not complete properly
        unexpected behavior


 -->

<!-- ## 12. Dependency Injection(DI)

        -- Dependency = something your class needs to do its work
        Example:
        StockController needs ApplicationDBContext
        A service needs ILogger
        A repository needs DbContext
        So these are dependencies.

        -- Injection = providing it from outside
        Example:
        Instead of writing inside your controller:

        var context = new ApplicationDBContext(...); // without dependency injection

        ASP.NET Core gives it automatically:

        public StockController(ApplicationDBContext context) // with dependency injection
        {
             _context = context;
        }

        -- Why is DI important? (real reason)
        -- Problem without DI: Tight coupling
        -- If you create objects directly inside a class:
        -- class becomes tied to that exact implementation
        -- hard to replace
        -- hard to test
        Example: You cannot easily replace DB with Mock DB.

        -- With DI: Loose coupling
        -- Controller depends on abstraction, not concrete implementation.
        -- That’s why real projects use: IStockRepository instead of StockRepository

        -- DI Container in ASP.NET Core (Most important concept)
        -- ASP.NET Core has a built-in system called: Service Container / DI Container
        -- It does:
        1.Stores what services exist
        2.Creates objects when needed
        3.Injects them into constructors
        4.Manages their lifetime

        In Program.cs you register services:
        builder.Services.AddDbContext<ApplicationDBContext>();
        This line means: “Hey DI container, if anyone asks for ApplicationDBContext, you create it and provide it.”

        -- The DI lifecycle (step by step)
        -- Let’s use your example:

        Step 1: You register service
        In Program.cs:
        builder.Services.AddDbContext<ApplicationDBContext>(...);
        DI container now knows how to create ApplicationDBContext.

        Step 2: Request comes to controller endpoint
        User hits:GET /api/stock
        ASP.NET needs StockController.

        Step 3: ASP.NET Core tries to create controller object
        But your controller constructor requires:
        public StockController(ApplicationDBContext context)
        So framework says: “To create StockController, I need ApplicationDBContext first.”

        Step 4: DI container creates ApplicationDBContext
        It uses the rules from Program.cs to create context (SQL Server etc.)

        Step 5: It injects dependency
        Then controller is created like:
        new StockController(context);
        ✅ Done. Now your endpoint runs.

        -- Types of Injection in DI
        1.Constructor Injection (most common, best practice)
        2.Property injection (rare)
        3.Method injection (rare)

        --Constructor injection is best because:
        dependency is mandatory
        object cannot exist without it

        -- Service Lifetimes
        -- When you register services, you choose lifetime.
         1) Singleton
        -- One object instance for the entire app lifetime.
        -- Created once and Shared for all users/requests
        -- Use for:
        caching service
        configuration providers

        -- Not for DbContext because If it becomes singleton:
        multiple requests share same context
        data issues + thread safety issues

        2) Scoped (MOST IMPORTANT FOR WEB APPS)
        -- One object per HTTP request
        -- New instance per request
        -- Same instance reused inside request
        -- Best for:
        DbContext
        repositories
        EF Core context must be scoped.

        3) Transient
        -- New instance every time requested
        -- Use for:
        lightweight services
        stateless helpers

        -- Dependency Injection is a design pattern where dependencies of a class are provided by an external container instead of being created inside the class. ASP.NET Core uses a built-in DI container to manage services and lifetimes (Scoped, Singleton, Transient).


 -->

<!-- ## 13. Repository Pattern

        -- A Repository is a layer/class that acts like a data access manager.
        -- It contains all DB operations like : GetAll , GetById ,Create ,Update ,Delete
        -- So Controllers don’t directly talk to EF Core DbContext.

        -- Problem without Repository
        If your controller uses _context directly:
        DB queries spread across controllers
        repeated logic in multiple places
        hard to test
        hard to maintain when project grows
        Example: If tomorrow you change database or add caching, you must edit many controllers.

        -- With Repository
        -- Controller becomes clean and simple:
        -- Controller only handles:
        ✅ HTTP requests
        ✅ status codes
        ✅ DTO mapping
        Repository handles:
        ✅ database logic only

        -- Best clean architecture:
        -- Controller → Repository Interface → Repository Implementation → DbContext → Database
        -- Where:
           Controller depends on abstraction
           Repository hides DB logic

        -- We create an interface first: IStockRepository
        -- Interface defines what the repo can do.
        -- Why interface?
        1.Because it gives abstraction.
        2.Tomorrow you can replace repository with:
        3.MongoDB implementation
        4.Dapper implementation
        5.In-memory repository for testing
        6.Controllers won’t change.

        -- Repository + DTO (Important rule)
        -- Best practice:
        Repository returns Entity
        Controller maps Entity → DTO

        -- so we use repository pattern in Interface and Repository files and then in Controller
        -- and we add service in program.cs

 -->

 <!-- ## 14. NewtonSoftJson
    

        -- download packages using nuget gallery 
        1.<PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="8.0.23" />
        2.<PackageReference Include="Newtonsoft.Json" Version="13.0.5-beta1" />

        -- Add below line in program.cs before dbContext connection
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        1) builder.Services.AddControllers()

        This registers Controllers as services and enables:
        ✅ controller routing
        ✅ model binding
        ✅ returning JSON responses
        ✅ attributes like [HttpGet], [HttpPost]
        So without AddControllers(), your controller endpoints won’t work.


        2) .AddNewtonsoftJson(...)
        ASP.NET Core by default uses System.Text.Json serializer.
        But when you add: AddNewtonsoftJson()
        You are switching JSON serialization to: Newtonsoft.Json (Json.NET)
        Newtonsoft is older but still very popular because it has:
        ✅ more flexibility
        ✅ more mature options
        ✅ reference loop settings
        ✅ easy custom converters

        3) What is ReferenceLoopHandling?
        It tells Newtonsoft JSON serializer what to do when it finds circular references.

        4) What is a Reference Loop? (Very important)
        In your project, this relationship exists:
        Stock model contains: public List<Comment> Comments { get; set; }
        Comment model contains: public Stock Stock { get; set; }

        So imagine JSON output:
        Stock includes Comments ✅
        Each Comment includes Stock ✅
        That Stock again includes Comments ✅
        That again includes Stock ✅
        This becomes: Infinite loop

        So serialization fails / crashes with error like: Self referencing loop detected

        5) What does ReferenceLoopHandling.Ignore do?
        Meaning:
        When Newtonsoft detects a loop, it will:
        ✅ ignore the looping property
        ✅ stop infinite recursion
        ✅ return JSON safely
        So it prevents the serializer from going endlessly:
        Stock → Comments → Stock → Comments → Stock...
        
        6) Practical meaning in API response
        If you return a Stock entity directly:
        Stock.Comments may serialize ✅
        but Comment.Stock (back reference) will be ignored ✅
        So JSON remains clean and no loop happens.
        
  -->

<!-- # 15. data validation
        -- Data Validation means checking incoming data (request body / route / query) to ensure it is:
        ✅ correct
        ✅ complete
        ✅ in valid format
        ✅ follows business rules

        -- Where should validation happen?
        There are 2 layers:
        ✅ 1) Client-side validation : (React / HTML form validation)

        ✅ 2) Server-side validation : (Asp.Net Core Web API)
        ✅ cannot be bypassed
        ✅ final authority for data correctness

        -- ASP.NET Core validates using Model Binding + Data Annotations.
        Flow:
        Client sends JSON
        ASP.NET converts JSON → DTO (model binding)
        Validation attributes are checked
        If invalid: API returns error response (usually 400)

        DataAnnotations (Validation Attributes)
        These are the most common validation rules:
        1. Required : field must be provided
        [Required]

        2. String length limit : prevent too large strings in DB
        [MaxLength(n)]
        [MinLength(n)]
        [StringLength(n)]

        3. Range : numeric value limit
        [Range(min, max)]

        4. Format validations : validate pattern
        [EmailAddress]
        [Phone]
        [Url]
        [RegularExpression("pattern")]

        5. Compare fields : confirm passwords etc.
        [Compare("OtherField")]

        -- DTO Validation (Best Practice)
        ✅ We validate DTOs, not Entities.
        Because:
        Entity = DB structure
        DTO = API contract

        So validations should be in:
        ✅ CreateStockDto
        ✅ UpdateStockRequestDto
        not in Stock entity.

        -- What is ModelState?
        ASP.NET Core stores validation results in:
        📌 ModelState
        If any validation fails:
        ✅ ModelState.IsValid == false

        example : check in Controllers

        -- [ApiController] and Automatic Validation
        ASP.NET Core automatically:
        ✅ checks DTO validations
        ✅ if invalid → returns 400 BadRequest automatically
        ✅ with full error details in response body
        So you usually DON’T need:
        if (!ModelState.IsValid) return BadRequest(ModelState);

        -- Route Validation
        [HttpGet("{id : int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        : so id will accept only integer values


 -->

 <!-- ## 16.ToList()
 
        .ToList() is a LINQ method that converts a sequence into a List<T>.
        📌 It executes the query and stores the result in memory as a list.

        Why is .ToList() used?
        Because many LINQ operations return lazy sequences like:
        ✅ IEnumerable<T> or IQueryable<T>
        But .ToList() forces evaluation and gives:
        ✅ List<T>

        -- .ToList() executes the query
        In EF Core:
        When you write:
        _context.Stocks

        that doesn’t hit the database yet.
        It’s only a query definition.

        ✅ Database is contacted only when query is executed, such as by:
        .ToList()
        .FirstOrDefault()
        .SingleOrDefault()
        .Count()
        .Any()
        -- So: .ToList() = “Run the SQL query now and give me the results.”

        -- Deferred Execution Concept
        Before .ToList() : LINQ query is not executed.
        Example theory:
        var q = _context.Stocks.Where(x => x.MarketCap > 1000);
        This doesn’t query DB yet.

        After .ToList()
        var result = q.ToList();
        ✅ Now SQL executes and result is fetched.

        .ToList() executes the LINQ query and converts the result into a List<T> stored in memory. 
  -->

<!-- ## 17. AsQueryable()
        Meaning : stocks becomes an IQueryable<Stock>
        📌 IQueryable means:
        ✅ query is still being built
        ✅ not executed yet
        ✅ filters can be added conditionally
 -->

 <!-- ## 18. search , filter , sort , pagination : in stock getAll route -->
