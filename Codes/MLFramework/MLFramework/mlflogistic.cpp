#include "mlflogistic.h"

#include "mlfmath.h"

#ifdef IRIS
#define ALPHA 0.02f
#else
#define ALPHA 0.001f
#endif

#pragma region MLFLogistic
MLFLogistic::MLFLogistic(std::vector<MLFData*> dataset, int thetaCount)
{
	this->thetaCount = thetaCount;
	theta = new float[this->thetaCount];
	for(int i = 0; i < this->thetaCount; i++)
	{
		theta[i] = 0.0f;
	}

	this->dataset = dataset;
}

MLFLogistic::~MLFLogistic(void)
{
	if(theta)
		delete[] theta;
}

void MLFLogistic::solve(void)
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

int MLFLogistic::test(void)
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

float MLFLogistic::hypothesis(float* x)
{
	float h = 0;
	for(int i = 0; i < thetaCount; i++)
	{
		float v = theta[i]*x[i];
		h += v;
	}
	h *= -1;
	h = (1 + pow(E, h));
	h = 1 / (float) h;
	return h;
}
#pragma endregion

MLFMultiLogistic::MLFMultiLogistic(std::vector<MLFData*> dataset, int numberClasses)
{
	this->dataset = dataset;
	this->numberClasses = numberClasses;
}

void MLFMultiLogistic::solve(void)
{

}