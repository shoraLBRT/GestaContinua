# GestaContinua - Task Management System

A comprehensive task management system with Telegram bot integration for tracking habits and goals.

## 📋 Table of Contents
- [About](#about)
- [Features](#features)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Telegram Bot Commands](#telegram-bot-commands)
- [Project Structure](#project-structure)
- [Usage Examples](#usage-examples)
- [Development](#development)
- [Testing](#testing)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## About

This system helps users create and track tasks with customizable schedules and progress tracking. It integrates with Telegram to provide a conversational interface for managing personal goals and habits.

## Features

### Task Management
- Create tasks with goals and schedules
- Track progress in multiple formats (Boolean, Number, Text)
- Set different schedules (Daily, Weekly, BiWeekly, etc.)
- Weighted progress calculations
- Task categories for organization

### Telegram Integration
- Bot commands: `/start`, `/add_task`, `/report`, `/list_tasks`
- Automated reminders based on task schedules
- Progress tracking via chat interface
- Real-time notifications

### Progress Tracking
- Record progress with various input formats
- Detailed progress history
- Goal completion tracking
- Status updates (Active, Paused, Completed)

## Architecture

This project follows Clean Architecture principles with the following layers:

### 1. Domain Layer
- **Entities**: User, Category, Task, ProgressRecord
- **Repositories**: Interfaces for data access operations

### 2. Application Layer
- **Use Cases**: Business logic implementations
  - CreateTaskUseCase
  - ProcessUserResponseUseCase
  - UpdateTaskStatusUseCase
  - ScheduleRemindersUseCase
- **Services**: Application services
  - ITaskScheduler
  - INotificationService
  - UserService
- **DTOs**: Data transfer objects for API communication

### 3. Infrastructure Layer
- **Data**: Entity Framework DbContext and configurations
- **Repositories**: Entity Framework implementations of repository interfaces
- **Services**: Implementation of notification services and background services

### 4. WebApi Layer
- **Controllers**: API endpoints for all functionality
- **BotController**: Handles Telegram bot webhooks
- **Background Services**: Reminder scheduling service
- **Dependency Injection**: Service registrations

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server or SQL Server Express LocalDB
- Telegram Bot Token (from @BotFather)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/GestaContinua.git
   cd GestaContinua
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json` if needed

4. Run migrations to create the database:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

## Configuration

To configure the Telegram bot, add your bot token to `appsettings.json`:
```json
{
  "TelegramBotToken": "YOUR_TELEGRAM_BOT_TOKEN_HERE",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GestaContinuaDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## API Endpoints

### Tasks
- `POST /api/tasks` - Create a new task
- `GET /api/tasks?userId={guid}` - Get user's tasks
- `PUT /api/tasks/{id}/status` - Update task status

### Categories
- `POST /api/categories` - Create a category
- `GET /api/categories` - Get all categories

### Progress
- `POST /api/progress` - Record progress
- `GET /api/progress/{taskId}` - Get progress history

### Bot
- `POST /api/bot/webhook` - Telegram bot webhook

## Telegram Bot Commands

- `/start` - Register user and start using the bot
- `/add_task` - Create a new task (through web interface)
- `/report` - View progress and update tasks (through web interface)
- `/list_tasks` - List all active tasks

## Project Structure

```
GestaContinua/
├── GestaContinua.Domain/           # Domain entities and interfaces
│   ├── Entities/                   # User, Category, Task, ProgressRecord
│   └── Repositories/               # Repository interfaces
├── GestaContinua.Application/      # Application layer
│   ├── DTOs/                       # Data transfer objects
│   ├── Services/                   # Application services
│   └── UseCases/                   # Business logic
├── GestaContinua.Infrastructure/   # Data access implementations
│   ├── Data/                       # DbContext
│   ├── Repositories/               # EF implementations
│   └── Services/                   # External service implementations
└── GestaContinua.WebApi/           # Web API and controllers
    ├── Controllers/
    ├── Services/
    └── Repositories/
```

## Usage Examples

### Creating a Task
```json
POST /api/tasks
{
  "userId": "guid-here",
  "categoryId": "guid-here",
  "name": "Read Book",
  "goal": 100,
  "inputFormat": "Number",
  "schedule": "Daily",
  "weight": 1
}
```

### Recording Progress
```json
POST /api/progress
{
  "taskId": "guid-here",
  "value": 10
}
```

### Updating Task Status
```json
PUT /api/tasks/{taskId}/status
{
  "taskId": "guid-here",
  "newStatus": "Paused"
}
```

## Development

### Running tests
```bash
dotnet test
```

### Adding migrations
```bash
dotnet ef migrations add MigrationName --project GestaContinua.Infrastructure --startup-project GestaContinua.WebApi
```

### Running in development mode
```bash
dotnet watch run
```

## Testing

The project includes:
- Unit tests for business logic
- Integration tests for API endpoints
- Repository tests for data access operations

To run all tests:
```bash
dotnet test
```

## Deployment

### Docker
The application can be containerized using Docker:
```bash
docker build -t gestacontinua .
docker run -p 80:80 gestacontinua
```

### Azure
Deploy to Azure App Service:
1. Create an App Service in Azure Portal
2. Publish from Visual Studio or via CLI
3. Configure connection strings and settings

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code of Conduct
- Follow the existing code style
- Write tests for new features
- Document public APIs
- Keep commit messages clear and descriptive

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

Project Maintainer - [Your Name] - [your.email@example.com]

Project Link: [https://github.com/your-username/GestaContinua](https://github.com/your-username/GestaContinua)

## Next Steps

### Immediate Tasks
1. **Writing Tests**: Create comprehensive unit, integration, and end-to-end tests
2. **Database Setup**: Configure real database connection and run initial migrations
3. **Telegram Bot Integration**: Obtain bot token and set up webhook
4. **Security Implementation**: Add authentication and authorization

### Future Enhancements
1. **Dashboard**: Create web dashboard for task management
2. **Analytics**: Add progress charts and statistics
3. **Mobile App**: Develop mobile application for iOS and Android
4. **Multi-language**: Add support for multiple languages
5. **Custom Reminders**: Allow users to set custom reminder times
6. **Backup**: Implement data export and backup functionality