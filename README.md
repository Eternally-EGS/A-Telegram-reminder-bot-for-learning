# Eternally16Remindme Bot

My first full-fledged Telegram bot in C#. It helps not to forget about important events: you add a date, and every day the bot sends you a report on how many days are left.

👉 **To try:** [@Eternally16Remindme_bot](https://t.me/Eternally16Remindme_bot)

---

## ✨ Opportunities

- /add YYYY-MM-DD Text' — add a new event
- /list' — show a list of all events
- /delete number' — delete an event by its number from the list
- Daily automatic mailing at 9 a.m. (Moscow time)

---

## 🛠️ Technology stack

- C# / .NET 8
- Telegram.Bot
- SQLite
- Amvera Cloud (hosting)

---

## Deployment

The project is set up for deployment via **GitHub + Amvera**. When you push it to the `master`, it is automatically assembled and deployed on the server.

### Environment variables

- `TELEGRAM_BOT_TOKEN' — the bot token received from [@BotFather](https://t.me/BotFather )

---

## 📂 Project structure

├── Commands/ # Command handlers (/start, /add, /list, /delete)
├── Host.cs # Class for managing the bot and background tasks

Program.cs # Entry point, launch of the web server and bot

└── reminders.db # SQLite database (created automatically)

---

## 👨‍💻 Author
 
GitHub: [Eternally-EGS](https://github.com/Eternally-EGS)

---

The bot was created in the process of learning C# and deployment .NET applications to the cloud._

