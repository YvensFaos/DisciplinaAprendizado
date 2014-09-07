#include <cstdlib>
#include <stdio.h>

#include "mlfreader.h"
#include "mlflinear.h"

void main(void)
{
	char* path = "C:/Users/Yvens/Documents/GitHub/DisciplinaAprendizado/Codes/MLFramework/Datasets/";
	//char* file = "iris.txt";
	char* file = "homes.txt";

	MLFReader* reader = new MLFReader(path, file);
	int thetaCount = 0;
	std::vector<MLFData*> dataset = reader->readDataset(&thetaCount);
	delete reader;

	MLFLinear* linear = new MLFLinear(dataset, thetaCount);
	linear->solve();

	delete linear;
	system("pause");
}