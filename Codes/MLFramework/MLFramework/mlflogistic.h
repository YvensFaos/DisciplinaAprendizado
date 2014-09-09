#ifndef __MLF_LOGISTIC__
#define __MLF_LOGISTIC__

#include "mlfdataset.h"

class MLFLogistic
{
public:
	std::vector<MLFData*> dataset;
	float* theta;
	int thetaCount;
	int c0;

public:
	MLFLogistic(void);
	MLFLogistic(std::vector<MLFData*> dataset, int thetaCount, int c0);
	~MLFLogistic(void);

	void solve(void);
	int test(void);

	void initialize(std::vector<MLFData*> dataset, int thetaCount, int c0);
	float hypothesis(float* x);
};

class MLFMultiLogistic
{
public:
	std::vector<MLFData*> dataset;
	int thetaCount;
	int numberClasses;

private:
	MLFLogistic* solvers;

public:
	MLFMultiLogistic(std::vector<MLFData*> dataset, int thetaCount, int numberClasses);
	~MLFMultiLogistic(void);

	void solve(void);
	int test(void);
};
#endif