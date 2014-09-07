#ifndef __MLF_READER__
#define __MLF_READER__

#include <stdio.h>

#include "mlfdataset.h"

class MLFReader
{
private:
	FILE* fileHandle;
public:
	MLFReader(void);
	MLFReader(char* path, char* filename);
	~MLFReader(void);

	std::vector<MLFData*> readDataset(int* thetaCount);
private:
	void initialize(char* path, char* filename);
};

#endif