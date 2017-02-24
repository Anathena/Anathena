namespace Ana.Source.ActionScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class that defines how a task dependency should be handled.
    /// </summary>
    internal class DependencyBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBehavior" /> class.
        /// </summary>
        public DependencyBehavior() : this(isDependencyRequiredForStart: true, isDependencyRequiredForUpdate: true, dependencies: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBehavior" /> class.
        /// </summary>
        /// <param name="dependencies">The classes or interfaces that this depends on.</param>
        public DependencyBehavior(params Type[] dependencies) : this(isDependencyRequiredForStart: true, isDependencyRequiredForUpdate: true, dependencies: dependencies)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBehavior" /> class.
        /// </summary>
        /// <param name="isDependencyRequiredForStart">Indicates whether the dependencies are required for starting the task.</param>
        /// <param name="isDependencyRequiredForUpdate">Indicates whether the dependencies are required for updating the task.</param>
        /// <param name="dependencies">The classes or interfaces that this depends on.</param>
        public DependencyBehavior(Boolean isDependencyRequiredForStart, Boolean isDependencyRequiredForUpdate, params Type[] dependencies)
        {
            this.IsDependencyRequiredForStart = isDependencyRequiredForStart;
            this.IsDependencyRequiredForUpdate = isDependencyRequiredForUpdate;
            this.Dependencies = (dependencies == null || dependencies.Any(x => x == null)) ? new HashSet<Type>() : new HashSet<Type>(dependencies);
        }

        /// <summary>
        /// Gets a value indicating whether the dependencies are required for starting the task.
        /// </summary>
        public Boolean IsDependencyRequiredForStart { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the dependencies are required for updating the task.
        /// </summary>
        public Boolean IsDependencyRequiredForUpdate { get; private set; }

        /// <summary>
        /// Gets or sets the tasks that this task depends on.
        /// </summary>
        private HashSet<Type> Dependencies { get; set; }

        /// <summary>
        /// Determines if all dependencies are resolved based on the provided list of completed dependencies.
        /// </summary>
        /// <param name="completedDependencies">All completed dependency types.</param>
        /// <returns>True if all dependencies are resolved, otherwise false.</returns>
        public Boolean AreDependenciesResolved(IEnumerable<Type> completedDependencies)
        {
            return this.Dependencies.Count <= 0 || this.Dependencies.All(x => completedDependencies.Any(y => x.IsAssignableFrom(y)));
        }
    }
    //// End class
}
//// End namespace
