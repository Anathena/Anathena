namespace Ana.Source.Main
{
    using System;

    /// <summary>
    /// This class contains properties that the main View can data bind to
    /// </summary>
    public class MainModel
    {
        /// <summary>
        /// Initializes a new instance of the MainModel class
        /// </summary>
        public MainModel()
        {
            this.ExampleValue = 0;
        }

        /// <summary>
        /// Gets or sets trash or whatever
        /// </summary>
        public Int32 ExampleValue { get; set; }
    }
    //// End class
}
//// End namespace