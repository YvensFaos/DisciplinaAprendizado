#include "mlflinear.h"

#define ALPHA 0.1f

MLFLinear::MLFLinear(std::vector<MLFData*> dataset, int thetaCount)
{
	this->thetaCount = thetaCount + 1;
	theta = new float[this->thetaCount];
	for(int i = 0; i < this->thetaCount; i++)
	{
		theta[i] = 0.0f;
	}

	this->dataset = dataset;
}

MLFLinear::~MLFLinear(void)
{
	if(theta)
		delete[] theta;
}

void MLFLinear::solve(void)
{
	/*std::vector<MLFData*>::const_iterator iterator;
	for (iterator = dataset.begin(); iterator != dataset.end(); ++iterator) {
		(*iterator)->print();
	}*/

	int size = dataset.size;
	int trainingSet = size*0.3f;

	float* nTheta = new float[thetaCount];

	for(int i = 0; i < trainingSet; i++)
	{

	}
}

float MLFLinear::hypothesis(float* x)
{
	float h = theta[0];
	for(int i = 0; i < thetaCount; i++)
	{
		h += theta[i + 1]*x[i];
	}

	return h;
}