#ifndef __MLF_MATH__
#define __MLF_MATH__

#define CLOSE_RANGE 0.05

class MLFMath
{
public:
	static bool checkClose(float v1, float v2)
	{
		return ((v1 + CLOSE_RANGE > v2 && v1 - CLOSE_RANGE < v2) || (v2 + CLOSE_RANGE > v1 && v2 - CLOSE_RANGE < v1));
	}
};

#endif