/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#pragma once

#define stackalloc(type, length) stack_array<type>::from_stack_ptr(reinterpret_cast<type*>(_malloca(sizeof(type) * length)), length)

template<typename T>
struct stack_array_ref
{
	explicit stack_array_ref(T *right, size_t length, bool on_stack)
		:	ptr(right),
			len(length),
			on_stack(on_stack)
	{
	}

	T *ptr;
	size_t len;
	bool on_stack;
};

template<typename T>
class stack_array
{
private:
	T* ptr;
	size_t len;
	bool on_stack;

	explicit stack_array(T* memory, size_t length) throw()
		:	len(length),
			ptr(memory),
			on_stack(true)
	{
	}

public:
	explicit stack_array(size_t length = 0) throw()
		:	len(length),
			ptr(new T[length]),
			on_stack(false)
	{
	}

	stack_array(stack_array<T>& right) throw()
		:	ptr(right.ptr),
			len(right.len),
			on_stack(right.on_stack)
	{
		right.ptr = NULL;
		right.len = 0;
		right.on_stack = false;
	}

	stack_array(stack_array_ref<T> right) throw()
	{
		ptr = right.ptr;
		len = right.len;
		on_stack = right.on_stack;

		right.ptr = NULL;
	}

	~stack_array()
	{
		if (on_stack)
			_freea(ptr);
		else
			delete[] ptr;
	}

	static stack_array<T> from_stack_ptr(T* memory, size_t length)
	{
		return stack_array<T>(memory, length);
	}

	operator stack_array_ref<T>() throw()
	{
		stack_array_ref<T> ans(ptr, len, on_stack);
		ptr = NULL;
		len = 0;
		on_stack = false;

		return ans;
	}

	stack_array<T>& operator = (stack_array<T>& right) throw()
	{
		if (right.ptr != ptr)
		{
			if (on_stack)
				_freea(ptr);
			else
				delete[] ptr;
		}

		ptr = right.ptr;
		len = right.len;
		on_stack = right.on_stack;

		right.ptr = NULL;
		right.len = 0;
		right.on_stack = false;

		return *this;
	}

	stack_array<T>& operator = (stack_array_ref<T> right) throw()
	{
		if (right.ptr != ptr)
		{
			if (on_stack)
				_freea(ptr);
			else
				delete[] ptr;
		}

		ptr = right.ptr;
		len = right.len;
		on_stack = right.on_stack;

		return *this;
	}

	const T* get() const
	{
		return ptr;
	}

	T* get() throw()
	{
		return ptr;
	}

	size_t size() const throw()
	{
		return len;
	}

	T& operator [] (size_t index)
	{
		return ptr[index];
	}

	const T& operator [] (size_t index) const
	{
		return ptr[index];
	}
};