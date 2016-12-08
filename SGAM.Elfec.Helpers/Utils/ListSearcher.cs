using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SGAM.Elfec.Helpers.Utils
{
    public class ListSearcher<T>
    {
        private readonly BackgroundWorker _worker;
        private string _searchQuery;

        public IEnumerable<T> BaseList { get; }
        public Func<T, string, bool> SearchFilter { get; }
        public event EventHandler<IEnumerable<T>> SearchCompleted;

        public class ListSearchWrapper
        {
            private readonly IEnumerable<T> _list;

            internal ListSearchWrapper(IEnumerable<T> list)
            {
                if (list == null)
                    throw new NullReferenceException("The provided list can't be null");
                _list = list;
            }

            public ListSearcher<T> With(Func<T, string, bool> searchPredicate)
            {
                if (searchPredicate == null)
                    throw new NullReferenceException("The provided searchPredicate can't be null");
                return new ListSearcher<T>(_list, searchPredicate);
            }
        }

        public static ListSearchWrapper For(IEnumerable<T> list)
        {
            return new ListSearchWrapper(list);
        }

        protected ListSearcher(IEnumerable<T> baseList, Func<T, string, bool> searchFilter)
        {
            BaseList = baseList;
            SearchFilter = searchFilter;
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += Filter;
            _worker.RunWorkerCompleted += FilterComplete;
        }

        public void Search(string searchQuery)
        {
            _searchQuery = searchQuery ?? "";
            if (!_worker.IsBusy)
            {
                _worker.RunWorkerAsync(_searchQuery);
                return;
            }
            if (_worker.CancellationPending) return;
            _worker.RunWorkerCompleted += RefilterOnCompletion;
            _worker.CancelAsync();
        }

        private void RefilterOnCompletion(object sender, RunWorkerCompletedEventArgs e)
        {
            _worker.RunWorkerCompleted -= RefilterOnCompletion;
            _worker.RunWorkerAsync(_searchQuery);
        }


        private void Filter(object sender, DoWorkEventArgs e)
        {
            var query = ((string)e.Argument).Replace("  ", " ").Trim().ToLowerInvariant();
            e.Result = string.IsNullOrWhiteSpace(query)
                ? BaseList
                : BaseList.Where(item => SearchFilter(item, query));
        }

        private void FilterComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            var filtered = (IEnumerable<T>)e.Result;
            SearchCompleted?.Invoke(this, filtered);
        }
    }
}