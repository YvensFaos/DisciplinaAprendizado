#ifndef __MLF_READER__
#define __MLF_READER__

class MLFReader
{
public:
	MLFReader(void);
	MLFReader(char* path, char* filename);
	~MLFReader(void);

private:
	void initialize(char* path, char* filename);
};

#endif