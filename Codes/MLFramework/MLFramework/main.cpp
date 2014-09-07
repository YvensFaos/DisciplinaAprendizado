#include <cstdlib>
#include <stdio.h>

#include "mlfreader.h"
#include "mlflinear.h"

void main(void)
{
	char* path = "C:/Users/Yvens/Documents/GitHub/DisciplinaAprendizado/Codes/MLFramework/Datasets/";
	char* file = "iris.txt";

	MLFReader reader = MLFReader(path, file);
	int thetaCount = 0;
	std::vector<MLFData*> dataset = reader.readDataset(&thetaCount);

	MLFLinear linear = MLFLinear(dataset, thetaCount);
	linear.solve();

	system("pause");
}