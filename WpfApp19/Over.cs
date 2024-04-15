using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp19
{
    partial class Action
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    partial class Logs
    {
        public override string ToString()
        {
            if (Users.Login != null || Action.Name != null || Date_ != null || Description != null)
            {
                return $"Пользователь: {Users.Login}, Действие: {Action.Name}, Время: {Date_}, Подробности: {Description}";
            }
            else
            {
                return null;
            }

        }
    }

    partial class Roles
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    partial class Users
    {
        public override string ToString()
        {
            return $"Логин: {Login}, ФИО: {Name}";
        }
    }

    partial class Category
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    partial class Dishes
    {
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    partial class Orders
    {
        public override string ToString()
        {
            return $"{Dishes.Name}, {Users.Login}, {Date_}, {Status}";
        }
    }

    partial class Entities : DbContext
    {
        public static Entities _context;
        public static Entities GetContext()
        {
            if (_context == null)
            {
                _context = new Entities();

            }
            return _context;
        }
    }
}
