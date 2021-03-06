#ifndef __MLF_DATASET__
#define __MLF_DATASET__

#include <vector>

class MLFData
{
public:
	float* value;
	int valuesLength;
	char* category;
	float yvalue;

public:
	MLFData(void);
	MLFData(float* value, int valuesLength, char* category, float yvalue);
	~MLFData(void);

	void print(void);
};

class MLFDataset
{
private:
	std::vector<MLFData*> dataset;
	char** categories;
	int categoriesLength;

public:
	MLFDataset(std::vector<MLFData*> dataset, char** categories, int categoriesLength);
	~MLFDataset(void);

	static std::vector<MLFData*> deepCopy(std::vector<MLFData*> dataset);
};

#endif