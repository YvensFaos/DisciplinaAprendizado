#include "mlfreader.h"

#include <stdio.h>
#include <vector>
#include <algorithm>

#define IRIS

MLFReader::MLFReader(void)
{ 
	initialize(nullptr, nullptr);
}

MLFReader::MLFReader(char* path, char* filename)
{
	initialize(path, filename);
}

MLFReader::~MLFReader(void)
{
	if(fileHandle)
		fclose(fileHandle);
}

void MLFReader::initialize(char* path, char* filename)
{
	char filepath[256];
	sprintf(filepath,"%s%s",path, filename);

	fileHandle = fopen(filepath, "rb");
	fseek(fileHandle, 0, SEEK_END);
	long fileSize = ftell(fileHandle);
	long filePos = 0;
	fseek(fileHandle, filePos, SEEK_SET);
}

std::vector<MLFData*> MLFReader::readDataset(int* thetaCount)
{
	char line[256];
	char buffer[4096];
	int ret = 0;
	int state = 0;
	std::vector<MLFData*> dataset;

#ifdef IRIS
	char* categories[3];
	categories[0] = "Iris-setosa";
	categories[1] = "Iris-versicolor";
	categories[2] = "Iris-virginica";

	int categoriesLength = 3;

	float v1 = 0;
	float v2 = 0; 
	float v3 = 0;
	float v4 = 0;
	char category[256];

	*thetaCount = 4;
	float values[4];
	int k = 0;
	float yvalue = 0;
	MLFData* data = nullptr;
	while(!feof(fileHandle))
    {
        memset(buffer, 0, 4096);
		fscanf(fileHandle, "%f,%f,%f,%f,%s",&v1,&v2,&v3,&v4,category);
		k = 0;
		values[k++] = v1;
		values[k++] = v2;
		values[k++] = v3;
		values[k++] = v4;

		if(category[5] == 's')
		{
			yvalue = 0;
		}
		else
		{
			if(category[6] == 'e')
			{
				yvalue = 1;
			}
			else
			{
				yvalue = 2;
			}
		}

		data = new MLFData(values, *thetaCount, category, yvalue);
		dataset.push_back(data);
	}
#endif

	std::random_shuffle(dataset.begin(), dataset.end());
	
	printf("Size = %d\n", dataset.size());

	return dataset;
}