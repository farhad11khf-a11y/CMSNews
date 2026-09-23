# CMSNews Project Context

## Project
This is an educational News CMS project built with ASP.NET MVC 5.

## Technology
- C#
- ASP.NET MVC 5
- .NET Framework 4.8
- Entity Framework 6
- SQL Server
- HTML
- CSS
- JavaScript
- Git / GitHub

## My programming level
I am a beginner.
Explain technical concepts in Persian and step by step.
Do not assume that I already understand advanced concepts.

## Architecture

The project uses a layered architecture:

Controller
↓
Service
↓
Repository
↓
DbContext
↓
Database

## Main layers

### Models
Contains entity classes and ViewModels.

Important entities include:
- News
- NewsGroup
- Comment
- User

### DbContext
The project uses Entity Framework DbContext to communicate with SQL Server.

### Repository
The Repository layer handles database operations.

Important interfaces/classes include:
- IGenericRepository<T>
- GenericRepository<T>
- INewsGroupRepository
- NewsGroupRepository

### Service
The Service layer contains business logic.

Important interfaces/classes include:
- IGenericService<T>
- GenericService<T>
- INewsService
- NewsService
- INewsGroupService
- NewsGroupService

### Controllers
Controllers receive HTTP requests and communicate with Services.

Important controllers include:
- HomeController
- NewsController
- NewsGroupsController

### Views
The project uses Razor Views.

Important views currently being studied are the NewsGroups views:
- Index
- Create
- Edit
- Details
- Delete

## Current learning focus

I am currently learning:
- Repository Pattern
- Service Layer
- Entity Framework
- CRUD
- MVC Controllers
- Razor Views
- DropDownList
- File Upload
- Git and GitHub

## Important learning rule

This is partly an educational project based on an instructor's course.

When analyzing instructor code:
1. First explain what the instructor's code does.
2. Do not immediately replace it with a different architecture.
3. If there is a better approach, explain it separately.
4. Clearly distinguish:
   - Instructor's educational approach
   - Better approach for a real-world project

## Current NewsGroup issue

The NewsGroup section uses a manually generated NewsGroupId based on the maximum existing ID.

The current approach is conceptually:

Get the maximum NewsGroupId
↓
Add 1
↓
Use the result for the new NewsGroup

This is being studied because it is part of the instructor's project.

Do not automatically change this approach unless asked.
If discussing production-quality code, explain the concurrency and database-key considerations separately.

## How I want to learn

I do not want to simply copy code.

For important concepts:
- Explain why the code exists.
- Explain what problem it solves.
- Explain what happens if it is removed.
- Explain how it connects to other layers.
- Give small examples when useful.

When possible, let me try to solve a problem before giving the complete solution.

## Git

The project is also being tracked with Git and GitHub.

When explaining Git commands:
- Explain what the command means in simple Persian.
- Explain what it does.
- Give an example related to this project.

## Current goal

The short-term goal is to finish learning the CMS News project.

After that, I want to use the knowledge to build real projects and eventually work on paid ASP.NET MVC projects.

## Important instruction for the AI

Do not assume that every existing implementation is the best possible implementation.

At the same time, do not unnecessarily rewrite the educational project.

Teach the current implementation first, then explain improvements when relevant.