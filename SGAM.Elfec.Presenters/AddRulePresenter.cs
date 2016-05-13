using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AddRulePresenter : BasePresenter<IAddRuleView>
    {
        public AddRulePresenter(IAddRuleView view, Policy policy, Rule rule = null) : base(view)
        {
            Rule = rule == null ? new Rule()
            { Entities = new ObservableCollection<IEntity>() }
                        : ObjectCloner.Clone(rule);
            _policy = policy;
            LoadEntities();
        }

        #region Private Attributes
        private ObservableCollection<IEntity> _entities;
        private IEntity _entityToAdd;
        private string _entityName;
        private Rule _rule;
        private Policy _policy;
        #endregion
        #region Properties
        public ObservableCollection<IEntity> Entities
        {
            get { return _entities; }
            set
            {
                _entities = value;
                RaisePropertyChanged("Entities");
            }
        }

        public IEntity EntityToAdd
        {
            get { return _entityToAdd; }
            set
            {
                _entityToAdd = value;
                RaisePropertyChanged("EntityToAdd");
            }
        }

        public string EntityName
        {
            get { return _entityName; }
            set
            {
                _entityName = value;
                RaisePropertyChanged("EntityName");
            }
        }

        public Rule Rule
        {
            get { return _rule; }
            set
            {
                _rule = value;
                RaisePropertyChanged("Rule");
            }
        }

        #endregion
        #region Commands
        public ICommand AddEntityCommand { get { return new DelegateCommand(AddEntity); } }
        public ICommand DeleteEntityCommand { get { return new ListItemCommand<IList>(DeleteEntity); } }
        public ICommand RegisterRuleCommand { get { return new DelegateCommand(RegisterRule); } }
        #endregion
        #region Private Methods

        /// <summary>
        /// Carga todas las entidades
        /// </summary>
        private void LoadEntities()
        {
            EntitiesManager.GetAllEntities()
                .Subscribe(
                (entities) =>
                {
                    Entities = entities.ToObservableCollectionAsync();
                },
                (error) =>
                {
                    Debug.WriteLine(error.Message);
                    //show error
                });
        }
        /// <summary>
        /// Adds an entity to the rule
        /// </summary>
        private void AddEntity()
        {
            if (EntityToAdd != null && !Rule.Entities.Contains(EntityToAdd))
            {
                Rule.Entities.Add(EntityToAdd);
                Entities.Remove(EntityToAdd);
                EntityToAdd = null;
                EntityName = null;
            }
        }

        /// <summary>
        /// Deletes the selected entities from the rule
        /// </summary>
        /// <param name="selectedEntities"></param>
        private void DeleteEntity(IList selectedEntities)
        {
            var entitiesToDel = selectedEntities.Cast<IEntity>().ToList();
            if (entitiesToDel != null && entitiesToDel.Count > 0)
            {
                Rule.Entities.RemoveRange(entitiesToDel);
                Entities.AddRange(entitiesToDel);
                EntityToAdd = null;
                EntityName = null;
            }
        }

        /// <summary>
        /// Remotely Adds the rule to the server
        /// </summary>
        private void RegisterRule()
        {
            View.Validate();
            if (Rule.IsValid)
            {
                View.ProcessingData();
                PolicyManager.RegisterRule(_policy.Type, Rule)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe((u) =>
                    {
                        _policy.Rules.AddInOrder(Rule, (r => r.Name));
                        View.Success(Rule);
                    }, View.Error);
            }
            else View.NotifyErrorsInFields();
        }

        #endregion

        #region Public Methods
        public bool FilterEntities(string search, object item)
        {
            // Cast the value to an IEntity.
            IEntity entity = item as IEntity;
            if (entity != null)
                return (entity.Name.ToLower().Contains(search.ToLower()))
                    || (entity.Details.ToLower().Contains(search.ToLower()));
            // If no match, return false.
            return false;
        }
        #endregion
    }
}
