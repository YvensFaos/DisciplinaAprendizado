#ifndef __MLF_LINEAR__
#define __MLF_LINEAR__

#include "mlfdataset.h"

class MLFLinear
{
public:
	std::vector<MLFData*> dataset;
	float* theta;
	float theta0;
	int thetaCount;

public:
	MLFLinear(std::vector<MLFData*> dataset, int thetaCount);
	~MLFLinear(void);

	void solve(void);
private:
	float hypothesis(float* x);
};

#endif