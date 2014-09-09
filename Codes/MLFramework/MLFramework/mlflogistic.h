#ifndef __MLF_LOGISTIC__
#define __MLF_LOGISTIC__

#include "mlfdataset.h"

class MLFLogistic
{
public:
	std::vector<MLFData*> dataset;
	float* theta;
	int thetaCount;

public:
	MLFLogistic(std::vector<MLFData*> dataset, int thetaCount);
	~MLFLogistic(void);

	void solve(void);
	int test(void);
private:
	float hypothesis(float* x);
};

class MLFMultiLogistic
{
public:
	std::vector<MLFData*> dataset;
	int numberClasses;

private:
	MLFLogistic* solvers;

public:
	MLFMultiLogistic(std::vector<MLFData*> dataset, int numberClasses);
	~MLFMultiLogistic(void);

	void solve(void);
	int test(void);
};
#endif