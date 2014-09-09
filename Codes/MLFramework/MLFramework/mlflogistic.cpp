#include "mlflogistic.h"

#include "mlfmath.h"

#ifdef IRIS
#define ALPHA 0.02f
#else
#define ALPHA 0.001f
#endif

#pragma region MLFLogistic
MLFLogistic::MLFLogistic(std::vector<MLFData*> dataset, int thetaCount, int c0)
{
	initialize(dataset, thetaCount, c0);
}

MLFLogistic::MLFLogistic(void)
{
	std::vector<MLFData*> dataset;
	initialize(dataset, -1, -1);
}

void MLFLogistic::initialize(std::vector<MLFData*> dataset, int thetaCount, int c0)
{
	this->thetaCount = thetaCount;
	theta = new float[this->thetaCount];
	for(int i = 0; i < this->thetaCount; i++)
	{
		theta[i] = 0.0f;
	}

	this->dataset = dataset;
	this->c0 = c0;
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

#pragma region MLFMultiLogistic
MLFMultiLogistic::MLFMultiLogistic(std::vector<MLFData*> dataset, int thetaCount, int numberClasses)
{
	this->dataset = dataset;
	this->thetaCount = thetaCount;
	this->numberClasses = numberClasses;
}

MLFMultiLogistic::~MLFMultiLogistic(void)
{
	if(solvers)
		delete[] solvers;
}

void MLFMultiLogistic::solve(void)
{
	int actualNumber = numberClasses;
	solvers = new MLFLogistic[numberClasses - 1];

	std::vector<MLFData*> adjustedDataset;
	int c0 = -1;
	int i = 0;
	while(actualNumber > 2)
	{
		for(int i = 0; i < dataset.size(); i++)
		{
			MLFData originalData = *dataset.at(i);
			if(c0 == -1)
			{
				c0 = originalData.yvalue;
			}
			float yvalue = (originalData.yvalue != c0)? 1 : 0;

			MLFData* data = new MLFData(originalData.value, originalData.valuesLength, originalData.category, yvalue);
			adjustedDataset.push_back(data);
		}

		solvers[i] = MLFLogistic(MLFDataset::deepCopy(adjustedDataset), thetaCount, c0);
		solvers[i].solve();

		adjustedDataset.clear();

	}
}

int MLFMultiLogistic::test(void)
{
	return 0;
}
#pragma endregion