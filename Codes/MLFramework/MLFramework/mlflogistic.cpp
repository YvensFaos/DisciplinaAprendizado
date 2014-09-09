#include "mlflogistic.h"

#include "mlfmath.h"

#ifdef IRIS
#define ALPHA 0.1f
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
	this->dataset = dataset;
	this->theta = nullptr;
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

	theta = new float[thetaCount];
	for(int i = 0; i < thetaCount; i++)
	{
		theta[i] = 0.0f;
	}

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
			if((theta[i] == nTheta[i]) || MLFMath::checkClose(theta[i], nTheta[i]))
			{
				equals++;
			}
			theta[i] = nTheta[i];
			printf("%f ",theta[i]);
		}
		printf("\n");

		if(equals >= (thetaCount - 1))
		{
			//printf("Finished!\n");
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

	std::vector<MLFData*> usingDatabase = MLFDataset::deepCopy(dataset);
	std::vector<MLFData*> adjustedDataset;
	int c0 = -1;
	int index = 0;
	MLFData* originalData;
	while(actualNumber >= 2)
	{
		for(int i = 0; i < usingDatabase.size(); i++)
		{
			originalData = usingDatabase.at(i);
			if(c0 == -1)
			{
				c0 = originalData->yvalue;
			}
			float yvalue = (originalData->yvalue != c0)? 1 : 0;

			adjustedDataset.push_back(new MLFData(originalData->value, originalData->valuesLength, originalData->category, yvalue));
		}

		solvers[index] = MLFLogistic(MLFDataset::deepCopy(adjustedDataset), thetaCount, c0);
		solvers[index].solve();
		index++;

		adjustedDataset.clear();
		//Buscando as linhas que não são do tipo de c0
		usingDatabase.clear();
		usingDatabase = MLFDataset::deepCopy(dataset);
		for(int i = 0; i < usingDatabase.size(); i++)
		{
			MLFData originalData = *usingDatabase.at(i);
			if(originalData.yvalue != c0)
			{
				adjustedDataset.push_back(new MLFData(originalData.value, originalData.valuesLength, originalData.category, originalData.yvalue));
			}
		}
		usingDatabase = MLFDataset::deepCopy(adjustedDataset);
		adjustedDataset.clear();
		c0 = -1;

		actualNumber--;
	}
}

int MLFMultiLogistic::test(void)
{
	int size = dataset.size();
	int result = 0;

	for(int i = 0; i < size; i++)
	{
		//printf("Results: %d > ", result);
		MLFData* data = dataset.at(i);
		for(int j = 0; j < numberClasses - 1; j++)
		{
			float h = solvers[j].hypothesis(data->value);

			//printf("[y = %f] h: %f\n", data->yvalue, h); 
			if(h < 0.5)
			{
				if(data->yvalue == solvers[j].c0)
				{
					result++;
					break;
				}
			}
			else
			{
				if(j == numberClasses - 2)
				{
					if(data->yvalue == (solvers[j].c0 + 1))
					{
						result++;
					}
				}
			}
		}

	}
	return result;
}
#pragma endregion