using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    public static class DisposableFactory
    {
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> fn)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return fn(disposable);
            }
        }

        public static void HandleUsing()
        {
            var data = Using(
                () => new System.Net.WebClient(),
                client =>
                {
                    return client.DownloadString("https://docs.microsoft.com/_api/familyTrees/bymoniker/net-6.0");
                });

            Console.WriteLine(data);
        }
    }
}
