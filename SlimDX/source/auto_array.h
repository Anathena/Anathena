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

template<class T>
struct auto_array_ref
{
	explicit auto_array_ref(T *right)
		: ref(right)
	{
	}

	T *ref;
};

template<class T>
class auto_array
{
private:
	T *myptr;

public:
	explicit auto_array(T *ptr = 0) throw()
		: myptr(ptr)
	{
	}

	auto_array(auto_array<T>& right) throw()
		: myptr(right.release())
	{
	}

	auto_array(auto_array_ref<T> right) throw()
	{
		T *ptr = right.ref;
		right.ref = NULL;
		myptr = ptr;
	}

	template<class Other>
	operator auto_array<Other>() throw()
	{
		return auto_array<Other>(*this);
	}

	template<class Other>
	operator auto_array_ref<Other>() throw()
	{
		Other *ptr = myptr;
		auto_array_ref<Other> ans(ptr);
		myptr = NULL;

		return ans;
	}

	template<class Other>
	auto_array<T>& operator = (auto_array<Other>& right) throw()
	{
		reset(right.release());
		return *this;
	}

	template<class Other>
	auto_array(auto_array<Other>& right) throw()
		: myptr(right.release())
	{
	}

	auto_array<T>& operator = (auto_array<T>& right) throw()
	{
		reset(right.release());
		return *this;
	}

	auto_array<T>& operator = (auto_array_ref<T> right) throw()
	{
		T *ptr = right.ref;
		right.ref = NULL;
		reset(ptr);

		return *this;
	}

	~auto_array()
	{
		delete[] myptr;
	}

	T& operator*() const throw()
	{
		return *get();
	}

	T *operator->() const throw()
	{
		return get();
	}

	T *get() const throw()
	{
		return myptr;
	}

	T *release() throw()
	{
		T *tmp = myptr;
		myptr = NULL;

		return tmp;
	}

	void reset(T *ptr = 0)
	{
		if (ptr != myptr)
			delete[] myptr;
		myptr = ptr;
	}
};