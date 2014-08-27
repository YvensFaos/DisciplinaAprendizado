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

	std::list<MLFData*> readDataset(void);
private:
	void initialize(char* path, char* filename);
};

#endif