namespace Ana.Source.Utils.Extensions
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Extension methods for converting and operating on <see cref="IntPtr"/> types.
    /// </summary>
    public static class IntPtrExtensions
    {
        /// <summary>
        /// Converts the given pointer to a <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The pointer converted to a <see cref="IntPtr"/>.</returns>
        public static unsafe IntPtr ToIntPtr(this UIntPtr intPtr)
        {
            return unchecked((IntPtr)intPtr.ToPointer());
        }

        /// <summary>
        /// Converts the given pointer to a <see cref="UIntPtr"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The pointer converted to a <see cref="UIntPtr"/>.</returns>
        public static unsafe UIntPtr ToUIntPtr(this IntPtr intPtr)
        {
            return unchecked((UIntPtr)intPtr.ToPointer());
        }

        /// <summary>
        /// Converts an <see cref="IntPtr"/> to an unsigned <see cref="UInt32"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static UInt32 ToUInt32(this IntPtr intPtr)
        {
            return unchecked((UInt32)(Int32)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="UIntPtr"/> to an unsigned <see cref="UInt32"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static UInt32 ToUInt32(this UIntPtr intPtr)
        {
            return unchecked((UInt32)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="IntPtr"/> to an unsigned <see cref="UInt64"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static UInt64 ToUInt64(this IntPtr intPtr)
        {
            return unchecked((UInt64)(Int64)intPtr);
        }

        /// <summary>
        /// Converts an <see cref="UIntPtr"/> to an unsigned <see cref="UInt64"/>.
        /// </summary>
        /// <param name="intPtr">The pointer to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static UInt64 ToUInt64(this UIntPtr intPtr)
        {
            return unchecked((UInt64)intPtr);
        }

        /// <summary>
        /// Performs the addition operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static IntPtr Add(this IntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left, right, ExpressionType.Add, wrapAround);
        }

        /// <summary>
        /// Performs the addition operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static UIntPtr Add(this UIntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left.ToIntPtr(), right, ExpressionType.Add, wrapAround).ToUIntPtr();
        }

        /// <summary>
        /// Performs the subtraction operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static IntPtr Subtract(this IntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left, right, ExpressionType.Subtract, wrapAround);
        }

        /// <summary>
        /// Performs the subtraction operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static UIntPtr Subtract(this UIntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left.ToIntPtr(), right, ExpressionType.Subtract, wrapAround).ToUIntPtr();
        }

        /// <summary>
        /// Performs the multiplication operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static IntPtr Multiply(this IntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left, right, ExpressionType.Multiply, wrapAround);
        }

        /// <summary>
        /// Performs the multiplication operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        public static UIntPtr Multiply(this UIntPtr left, dynamic right, Boolean wrapAround = true)
        {
            return IntPtrExtensions.DoOperation(left.ToIntPtr(), right, ExpressionType.Multiply, wrapAround).ToUIntPtr();
        }

        /// <summary>
        /// Performs the division operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <returns>The result of the operation.</returns>
        public static IntPtr Divide(this IntPtr left, dynamic right)
        {
            return IntPtrExtensions.DoOperation(left, right, ExpressionType.Multiply);
        }

        /// <summary>
        /// Performs the division operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <returns>The result of the operation.</returns>
        public static UIntPtr Divide(this UIntPtr left, dynamic right)
        {
            return IntPtrExtensions.DoOperation(left.ToIntPtr(), right, ExpressionType.Multiply).ToUIntPtr();
        }

        /// <summary>
        /// Performs the modulo operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <returns>The result of the operation.</returns>
        public static IntPtr Mod(this IntPtr left, dynamic right)
        {
            return IntPtrExtensions.DoOperation(left, right, ExpressionType.Modulo);
        }

        /// <summary>
        /// Performs the modulo operation with the given values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <returns>The result of the operation.</returns>
        public static UIntPtr Mod(this UIntPtr left, dynamic right)
        {
            return IntPtrExtensions.DoOperation(left.ToIntPtr(), right, ExpressionType.Modulo).ToUIntPtr();
        }

        /// <summary>
        /// Performs the given mathematical operation on the given left and right values.
        /// </summary>
        /// <param name="left">The left side value.</param>
        /// <param name="right">The right side value.</param>
        /// <param name="expression">The mathematical operation to perform.</param>
        /// <param name="wrapAround">Whether or not values will wrap if the operation overflows. Otherwise, cap out at IntPtr.MaxValue or IntPtr.MinValue.</param>
        /// <returns>The result of the operation.</returns>
        private static IntPtr DoOperation(IntPtr left, dynamic right, ExpressionType expression, Boolean wrapAround = true)
        {
            dynamic leftSide;
            dynamic rightSide;

            if (wrapAround)
            {
                switch (IntPtr.Size)
                {
                    case sizeof(Int32):
                        leftSide = left.ToUInt32();
                        rightSide = (UInt32)right;
                        break;
                    default:
                        leftSide = left.ToUInt64();
                        rightSide = (UInt64)right;
                        break;
                }
            }
            else
            {
                switch (IntPtr.Size)
                {
                    case sizeof(Int32):
                        leftSide = left.ToUInt32();
                        rightSide = unchecked((UInt32)right);
                        break;
                    default:
                        leftSide = left.ToUInt64();
                        rightSide = unchecked((UInt64)right);
                        break;
                }
            }

            try
            {
                switch (expression)
                {
                    case ExpressionType.Add:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return wrapAround ? unchecked((Int32)(leftSide + rightSide)).ToIntPtr() : checked((Int32)(leftSide + rightSide)).ToIntPtr();
                            default:
                                return wrapAround ? unchecked((Int64)(leftSide + rightSide)).ToIntPtr() : checked((Int64)(leftSide + rightSide)).ToIntPtr();
                        }

                    case ExpressionType.Subtract:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return wrapAround ? unchecked((Int32)(leftSide - rightSide)).ToIntPtr() : checked((Int32)(leftSide - rightSide)).ToIntPtr();
                            default:
                                return wrapAround ? unchecked((Int64)(leftSide - rightSide)).ToIntPtr() : checked((Int64)(leftSide - rightSide)).ToIntPtr();
                        }

                    case ExpressionType.Multiply:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return wrapAround ? unchecked((Int32)(leftSide * rightSide)).ToIntPtr() : checked((Int32)(leftSide * rightSide)).ToIntPtr();
                            default:
                                return wrapAround ? unchecked((Int64)(leftSide * rightSide)).ToIntPtr() : checked((Int64)(leftSide * rightSide)).ToIntPtr();
                        }

                    case ExpressionType.Divide:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return unchecked((Int32)(leftSide / rightSide)).ToIntPtr();
                            default:
                                return unchecked((Int64)(leftSide / rightSide)).ToIntPtr();
                        }

                    case ExpressionType.Modulo:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return unchecked((Int32)(leftSide % rightSide)).ToIntPtr();
                            default:
                                return unchecked((Int64)(leftSide % rightSide)).ToIntPtr();
                        }

                    default:
                        throw new Exception("Unknown operation");
                }
            }
            catch (OverflowException ex)
            {
                switch (expression)
                {
                    case ExpressionType.Add:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return rightSide >= 0 ? Int32.MaxValue.ToIntPtr() : Int32.MinValue.ToIntPtr();
                            default:
                                return rightSide >= 0 ? Int64.MaxValue.ToIntPtr() : Int64.MinValue.ToIntPtr();
                        }

                    case ExpressionType.Multiply:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return ((rightSide >= 0 && leftSide >= 0) || (rightSide <= 0 && leftSide <= 0)) ? Int32.MaxValue.ToIntPtr() : Int32.MinValue.ToIntPtr();
                            default:
                                return ((rightSide >= 0 && leftSide >= 0) || (rightSide <= 0 && leftSide <= 0)) ? Int64.MaxValue.ToIntPtr() : Int64.MinValue.ToIntPtr();
                        }

                    case ExpressionType.Subtract:
                        switch (IntPtr.Size)
                        {
                            case sizeof(Int32):
                                return rightSide >= 0 ? Int32.MinValue.ToIntPtr() : Int32.MaxValue.ToIntPtr();
                            default:
                                return rightSide >= 0 ? Int64.MinValue.ToIntPtr() : Int64.MaxValue.ToIntPtr();
                        }

                    default:
                        throw ex;
                }
            }
        }
    }
    //// End class
}
//// End namespace