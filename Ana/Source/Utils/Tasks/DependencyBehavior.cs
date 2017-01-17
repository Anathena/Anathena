namespace Ana.Source.Utils.Tasks
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
        public DependencyBehavior() : this(dependencyRequiredForStart: true, dependencyRequiredForUpdate: true, dependencies: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBehavior" /> class.
        /// </summary>
        /// <param name="dependencies">The classes or interfaces that this depends on.</param>
        public DependencyBehavior(params Type[] dependencies) : this(dependencyRequiredForStart: true, dependencyRequiredForUpdate: true, dependencies: dependencies)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyBehavior" /> class.
        /// </summary>
        /// <param name="dependencyRequiredForStart">Indicates whether the dependencies are required for starting the task.</param>
        /// <param name="dependencyRequiredForUpdate">Indicates whether the dependencies are required for updating the task.</param>
        /// <param name="dependencies">The classes or interfaces that this depends on.</param>
        public DependencyBehavior(Boolean dependencyRequiredForStart, Boolean dependencyRequiredForUpdate, params Type[] dependencies)
        {
            this.DependencyRequiredForStart = dependencyRequiredForStart;
            this.DependencyRequiredForUpdate = dependencyRequiredForUpdate;
            this.Dependencies = (dependencies == null || dependencies.Any(x => x == null)) ? new HashSet<Type>() : new HashSet<Type>(dependencies);
        }

        /// <summary>
        /// Gets a value indicating whether the dependencies are required for starting the task.
        /// </summary>
        public Boolean DependencyRequiredForStart { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the dependencies are required for updating the task.
        /// </summary>
        public Boolean DependencyRequiredForUpdate { get; private set; }

        /// <summary>
        /// Gets or sets the tasks that this task depends on.
        /// </summary>
        private HashSet<Type> Dependencies { get; set; }
    }
    //// End class
}
//// End namespace
