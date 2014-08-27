#include <cstdlib>
#include <stdio.h>

#include "mlfreader.h"

int main(void)
{
	char* path = "C:/Users/Yvens/Documents/GitHub/DisciplinaAprendizado/Codes/MLFramework/Datasets/";
	char* file = "iris.txt";

	MLFReader reader = MLFReader(path, file);
	reader.readDataset();

	system("pause");
	return 0;
}