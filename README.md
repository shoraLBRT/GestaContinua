# GestaContinua - Personal Data Management System

A personal project for self-tracking and life optimization through data persistence and analytics.

## 📋 Table of Contents
- [About](#about)
- [Features](#features)
- [Current Status](#current-status)
- [Roadmap](#roadmap)
- [License](#license)

## About

GestaContinua is a personal project born out of a desire to maintain a digital log of my own life experiences, habits, and progress. The primary goal is to create a system for storing personal data in a database with eventual capabilities for aggregation, analytics, and optimization of personal life patterns.

The system helps track tasks with customizable schedules and progress tracking, integrating with Telegram for a conversational interface to managing personal goals and habits. This is very much a work in progress and experimental in nature.

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

## Current Status

This project is currently under active development and should be considered experimental. Many features are not yet fully implemented, and the architecture may evolve significantly as requirements become clearer. The system is functional in a basic sense but not yet production-ready.

## Roadmap

### Completed Features
- Basic architecture setup following Clean Architecture principles
- Task creation and management
- Progress tracking functionality
- Basic Telegram bot integration
- Entity Framework data access layer

### In Development
- Database migration scripts and proper schema
- Authentication and user management
- Enhanced error handling and validation
- Unit and integration tests

### Planned Features
- Comprehensive dashboard with analytics and charts
- Advanced reporting and visualization of personal data
- Mobile application for iOS and Android
- Data export and backup functionality
- Enhanced notification and reminder system
- Customizable goal setting and achievement tracking
- Multi-language support
- Advanced scheduling options for tasks
- Personal insights and recommendation engine

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
