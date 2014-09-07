#include "mlflinear.h"

#define ALPHA 0.05f

MLFLinear::MLFLinear(std::vector<MLFData*> dataset, int thetaCount)
{
	this->thetaCount = thetaCount;
	theta = new float[this->thetaCount];
	for(int i = 0; i < this->thetaCount; i++)
	{
		theta[i] = 1.0f;
	}
	theta0 = 1.0f;

	this->dataset = dataset;
}

MLFLinear::~MLFLinear(void)
{
	if(theta)
		delete[] theta;
}

void MLFLinear::solve(void)
{
	int size = dataset.size();
	int trainingSet = size; //*0.3f;

	float* nTheta = new float[thetaCount];
	float* sums =   new float[thetaCount];
	float nTheta0 = 0.0f;

	float h;
	float hh = 0;
	while(true)
	{
		h = 0;
		for(int i = 0; i < trainingSet; i++)
		{
			MLFData* data = dataset.at(i);
			h += hypothesis(data->value) - data->yvalue;
		}
		h /= trainingSet;
		h *= ALPHA;

		nTheta0 = theta0 - h;

		for(int i = 0; i < thetaCount; i++)
		{
			h = 0;	
			for(int j = 0; j < trainingSet; j++)
			{
				MLFData* data = dataset.at(j);
				hh = hypothesis(data->value);
				h += (hh - data->yvalue)*data->value[i];
			}
			h /= trainingSet;
			h *= ALPHA;

			nTheta[i] = theta[i] - h;
		}

		theta0 = nTheta0;
		printf("%f ",theta0);

		for(int i = 0; i < thetaCount; i++)
		{
			theta[i] = nTheta[i];
			printf("%f ",theta[i]);
		}
		printf("\n");
	}
}

float MLFLinear::hypothesis(float* x)
{
	float h = theta0;
	for(int i = 0; i < thetaCount; i++)
	{
		h += theta[i]*x[i];
	}

	return h;
}