using Area.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class BackgroundJobService : IService
    {
        private readonly ApplicationDbContext _context;
        private readonly TriggerFactory _triggerFactory;

        public BackgroundJobService(ApplicationDbContext context, TriggerFactory triggerFactory)
        {
            _context = context;
            _triggerFactory = triggerFactory;
        }

        public void Start()
        {
            var thread = new Thread(new ThreadStart(this.ExecuteCore));
            thread.IsBackground = true;
            thread.Start();
        }

        private void ExecuteCore()
        {
            Console.WriteLine("BackgroundJob: Starting...");
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    var accounts = _context.Accounts.Include(t => t.Tokens).Include(t => t.Triggers).Where(t => t.Triggers.Count > 0);
                    foreach (var account in accounts)
                    {
                        Console.WriteLine("Checking triggers for account " + account.UserName);
                        foreach (var trigger in account.Triggers)
                        {
                            Console.WriteLine("Checking trigger...");
                            if (trigger.LastVerificationDate == null)
                                trigger.LastVerificationDate = DateTime.Now;
                            _triggerFactory.CreateTriggerTemplate(trigger);
                            trigger.Template.TryActivate(account, string.Empty, (DateTime)trigger.LastVerificationDate);
                            trigger.LastVerificationDate = trigger.Template.GetTriggerDate();
                            _context.Update(trigger);
                        }
                    }
                    try {
                        _context.SaveChanges();
                    }catch (Exception){
                    }
                }catch (Exception e)
                {
                    Console.WriteLine($"BackgroundJob: Stoping...\n{e.StackTrace}\n{e.Message}");
                    break;
                }
            }
            Console.WriteLine("BackgroundJob: Stoped");
        }
    }
}
