#include "mlfdataset.h"

MLFData::MLFData(float* value, int valuesLength, char* category, float yvalue)
{
	this->value = new float[valuesLength];

	for(int i = 0; i < valuesLength; i++)
	{
		this->value[i] = value[i];
	}

	this->valuesLength = valuesLength;
	this->category = category;
	this->yvalue = yvalue;
}

void MLFData::print(void)
{
	for(int i = 0; i < valuesLength; i++)
	{
		printf("%4.2f ", value[i]);
	}
	printf("%s[%f]\n", category, yvalue);
}

MLFData::~MLFData(void)
{ 
	if(value)
		delete[] value;
}

//####

MLFDataset::MLFDataset(std::vector<MLFData*> dataset, char** categories, int categoriesLength)
{
	this->dataset = dataset;
	this->categories = categories;
	this->categoriesLength = categoriesLength;
}

MLFDataset::~MLFDataset(void)
{
	if(categories)
		delete[] categories;
}