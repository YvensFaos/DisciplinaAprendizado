#include "mlflinear.h"

#include "mlfmath.h"

#ifdef IRIS
#define ALPHA 0.02f
#else
#define ALPHA 0.001f
#endif

MLFLinear::MLFLinear(std::vector<MLFData*> dataset, int thetaCount)
{
	this->thetaCount = thetaCount;
	theta = new float[this->thetaCount];
	for(int i = 0; i < this->thetaCount; i++)
	{
		theta[i] = 0.0f;
	}
	theta0 = 0.f;

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
	int trainingSet = size*0.3f;

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
				hh = hh - data->yvalue;
				h += hh * data->value[i];
			}

			h /= trainingSet;
			h *= ALPHA;

			nTheta[i] = theta[i] - h;
		}

		theta0 = nTheta0;
		printf("%f ",theta0);

		int equals = 0;
		for(int i = 0; i < thetaCount; i++)
		{
			if((theta[i] == nTheta[i]))
			{
				equals++;
			}
			theta[i] = nTheta[i];
			printf("%f ",theta[i]);
		}
		printf("\n");

		if(equals >= (thetaCount - 1))
		{
			break;
		}
	}
}

int MLFLinear::test(void)
{
	int size = dataset.size();
	int result = 0;

	for(int i = 0; i < size; i++)
	{
		MLFData* data = dataset.at(i);
		float h = hypothesis(data->value);

		if(MLFMath::checkClose(h, data->yvalue) || ((int)h == (int)data->yvalue))
		{
			result++;
		}
	}

	return result;
}

float MLFLinear::hypothesis(float* x)
{
	float h = theta0;
	for(int i = 0; i < thetaCount; i++)
	{
		float v = theta[i]*x[i];
		h += v;
	}

	return h;
}