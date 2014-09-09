#include "mlfdataset.h"

MLFData::MLFData(void)
{
	this->value = nullptr;
	this->valuesLength = -1;
	this->category = "";
	this->yvalue = -1;
}

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

std::vector<MLFData*> MLFDataset::deepCopy(std::vector<MLFData*> dataset)
{
	std::vector<MLFData*> copy;

	for(int i = 0; i < dataset.size(); i++)
	{
		MLFData* data = dataset.at(i);
		copy.push_back(new MLFData(data->value, data->valuesLength, data->category, data->yvalue));
	}

	return copy;
}