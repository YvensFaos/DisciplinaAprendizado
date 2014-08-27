#include "mlfdataset.h"

MLFData::MLFData(float* value, int valuesLength, char* category)
{
	this->value = new float[valuesLength];

	for(int i = 0; i < valuesLength; i++)
	{
		this->value[i] = value[i];
	}

	this->valuesLength = valuesLength;
	this->category = category;
}

MLFData::~MLFData(void)
{ 
	if(value)
		delete[] value;
}

//####

MLFDataset::MLFDataset(std::list<MLFData*> dataset, char** categories, int categoriesLength)
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