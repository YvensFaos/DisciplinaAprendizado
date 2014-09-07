#include <cstdlib>
#include <stdio.h>

#include "mlfreader.h"
#include "mlflinear.h"

//#define IRIS
//#define HOMES
//#define NORMALIZE

void main(void)
{
	char* path = "C:/Users/Yvens/Documents/GitHub/DisciplinaAprendizado/Codes/MLFramework/Datasets/";
#ifdef IRIS
	char* file = "iris.txt";
#endif
#ifdef HOMES
	char* file = "homes.txt";
#endif
#ifdef HOUSES
	char* file = "houses.txt";
#endif

	MLFReader* reader = new MLFReader(path, file);
	int thetaCount = 0;
	std::vector<MLFData*> dataset = reader->readDataset(&thetaCount);
	delete reader;

	MLFLinear* linear = new MLFLinear(dataset, thetaCount);
	linear->solve();
	int results = linear->test();

	printf("Results: %d/%d\n", results, dataset.size());

	delete linear;
	system("pause");
}