﻿namespace Squalr.Source.Debugger.Disassembly
{
    using GalaSoft.MvvmLight.CommandWpf;
    using Squalr.Engine;
    using Squalr.Engine.Architecture;
    using Squalr.Engine.Architecture.Disassembler;
    using Squalr.Engine.DataStructures;
    using Squalr.Engine.Processes;
    using Squalr.Engine.VirtualMachines;
    using Squalr.Source.Docking;
    using Squalr.Source.ProjectExplorer;
    using Squalr.Source.ProjectItems;
    using Squalr.Source.Utils.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Disassembly.
    /// </summary>
    internal class DisassemblyViewModel : ToolViewModel, IProcessObserver
    {
        /// <summary>
        /// Singleton instance of the <see cref="DisassemblyViewModel" /> class.
        /// </summary>
        private static Lazy<DisassemblyViewModel> disassemblyViewModelInstance = new Lazy<DisassemblyViewModel>(
                () => { return new DisassemblyViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The base address from which dissassembly begins.
        /// </summary>
        private UInt64 baseAddress;

        /// <summary>
        /// The collection of visible instructions.
        /// </summary>
        private FullyObservableCollection<InstructionItem> instructions;

        /// <summary>
        /// The selected instructions.
        /// </summary>
        private IEnumerable<InstructionItem> selectedInstructions;

        /// <summary>
        /// Prevents a default instance of the <see cref="DisassemblyViewModel" /> class from being created.
        /// </summary>
        private DisassemblyViewModel() : base("Disassembly")
        {
            this.Instructions = new FullyObservableCollection<InstructionItem>();

            this.SelectInstructionsCommand = new RelayCommand<Object>((selectedItems) => this.SelectedInstructions = (selectedItems as IList)?.Cast<InstructionItem>(), (selectedItems) => true);
            this.AddInstructionCommand = new RelayCommand<InstructionItem>((instruction) => Task.Run(() => this.AddInstruction(instruction)), (scanResult) => true);
            this.AddInstructionsCommand = new RelayCommand<Object>((selectedItems) => Task.Run(() => this.AddInstructions(this.SelectedInstructions)), (selectedItems) => true);

            DockingViewModel.GetInstance().RegisterViewModel(this);

            Task.Run(() => Eng.GetInstance().Processes.Subscribe(this));
        }

        /// <summary>
        /// Gets or sets the command to select instructions.
        /// </summary>
        public ICommand SelectInstructionsCommand { get; private set; }

        /// <summary>
        /// Gets the command to add an instruction to the project explorer.
        /// </summary>
        public ICommand AddInstructionCommand { get; private set; }

        /// <summary>
        /// Gets the command to add instructions to the project explorer.
        /// </summary>
        public ICommand AddInstructionsCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected scan results.
        /// </summary>
        public IEnumerable<InstructionItem> SelectedInstructions
        {
            get
            {
                return this.selectedInstructions;
            }

            set
            {
                this.selectedInstructions = value;
                this.RaisePropertyChanged(nameof(this.SelectedInstructions));
            }
        }

        /// <summary>
        /// Gets or sets the base address from which dissassembly begins.
        /// </summary>
        public UInt64 BaseAddress
        {
            get
            {
                return this.baseAddress;
            }

            set
            {
                this.baseAddress = value;
                this.RaisePropertyChanged(nameof(this.BaseAddress));
            }
        }

        /// <summary>
        /// Gets or sets the collection of visible instructions.
        /// </summary>
        public FullyObservableCollection<InstructionItem> Instructions
        {
            get
            {
                return this.instructions;
            }

            set
            {
                this.instructions = value;
                this.RaisePropertyChanged(nameof(this.Instructions));
            }
        }

        /// <summary>
        /// Gets the disassembler.
        /// </summary>
        private IDisassembler Disassembler
        {
            get
            {
                return Eng.GetInstance().Architecture.GetDisassembler();
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="DisassemblyViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static DisassemblyViewModel GetInstance()
        {
            return disassemblyViewModelInstance.Value;
        }

        public void Update(NormalizedProcess process)
        {
            if (process != null)
            {
                this.BaseAddress = Eng.GetInstance().VirtualMemory.GetModules().FirstOrDefault()?.BaseAddress.ToUInt64() ?? 0UL;

                this.LoadInstructions();
            }
        }

        /// <summary>
        /// Adds the given instruction to the project explorer.
        /// </summary>
        /// <param name="instruction">The instruction to add to the project explorer.</param>
        private void AddInstruction(InstructionItem instruction)
        {
            ProjectExplorerViewModel.GetInstance().AddNewProjectItems(addToSelected: false, projectItems: instruction);
        }

        /// <summary>
        /// Adds the given instructions to the project explorer.
        /// </summary>
        /// <param name="instructions">The instructions to add to the project explorer.</param>
        private void AddInstructions(IEnumerable<InstructionItem> instructions)
        {
            if (instructions == null)
            {
                return;
            }

            ProjectExplorerViewModel.GetInstance().AddNewProjectItems(addToSelected: false, projectItems: instructions);
        }

        /// <summary>
        /// Loads the instructions to display.
        /// </summary>
        private void LoadInstructions()
        {
            Byte[] bytes = Eng.GetInstance().VirtualMemory.ReadBytes(this.BaseAddress.ToIntPtr(), 200, out _);

            if (bytes.IsNullOrEmpty())
            {
                return;
            }

            Boolean isProcess32Bit = Eng.GetInstance().Processes.IsOpenedProcess32Bit();

            // Disassemble instructions
            IEnumerable<NormalizedInstruction> disassembledInstructions = this.Disassembler.Disassemble(bytes, isProcess32Bit, this.BaseAddress.ToIntPtr());
            IList<InstructionItem> instructions = new List<InstructionItem>();

            foreach (NormalizedInstruction disassembledInstruction in disassembledInstructions)
            {
                String moduleName;
                UInt64 address = AddressResolver.GetInstance().AddressToModule(disassembledInstruction.Address, out moduleName);

                instructions.Add(new InstructionItem(address.ToIntPtr(), moduleName, disassembledInstruction.Instruction, disassembledInstruction.Bytes));
            }

            this.Instructions = new FullyObservableCollection<InstructionItem>(instructions);
        }
    }
    //// End class
}
//// End namespace