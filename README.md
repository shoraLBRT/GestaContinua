# GestaContinua - Task Management System

A comprehensive task management system with Telegram bot integration for tracking habits and goals.

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

## Main Features

### Task Management
- Create tasks with goals and schedules
- Track progress in multiple formats (Boolean, Number, Text)
- Set different schedules (Daily, Weekly, BiWeekly, etc.)

### Telegram Integration
- Bot commands: `/start`, `/add_task`, `/report`, `/list_tasks`
- Automated reminders based on task schedules
- Progress tracking via chat interface

### Progress Tracking
- Record progress with various input formats
- Detailed progress history
- Weighted progress calculations

## Setup Instructions

1. Restore packages:
   ```bash
   dotnet restore
   ```

2. Update the connection string in `appsettings.json` if needed

3. Run migrations to create the database:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. To configure the Telegram bot, add your bot token to `appsettings.json`:
   ```json
   {
     "TelegramBotToken": "your-telegram-bot-token"
   }
   ```

## API Endpoints

- `POST /api/tasks` - Create a new task
- `GET /api/tasks?userId={guid}` - Get user's tasks
- `PUT /api/tasks/{id}/status` - Update task status
- `POST /api/categories` - Create a category
- `GET /api/categories` - Get all categories
- `POST /api/progress` - Record progress
- `GET /api/progress/{taskId}` - Get progress history
- `POST /api/bot/webhook` - Telegram bot webhook

## Background Services

The application includes a background service that checks for tasks requiring reminders every 2 minutes and sends notifications via the Telegram bot.