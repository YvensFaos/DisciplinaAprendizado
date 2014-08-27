#ifndef __MLF_DATASET__
#define __MLF_DATASET__

#include <list>

class MLFData
{
public:
	float* value;
	int valuesLength;
	char* category;

public:
	MLFData(float* value, int valuesLength, char* category);
	~MLFData(void);
};

class MLFDataset
{
private:
	std::list<MLFData*> dataset;
	char** categories;
	int categoriesLength;

public:
	MLFDataset(std::list<MLFData*> dataset, char** categories, int categoriesLength);
	~MLFDataset(void);
};

#endif