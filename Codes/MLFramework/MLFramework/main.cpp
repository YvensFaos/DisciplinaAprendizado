#include <cstdlib>
#include <stdio.h>

#include "mlfreader.h"
#include "mlflinear.h"
#include "mlflogistic.h"

//#define IRIS
//#define HOMES
//#define NORMALIZE

void main(void)
{
	char* path = "C:/Users/Yvens/Documents/GitHub/DisciplinaAprendizado/Codes/MLFramework/Datasets/";
#ifdef IRIS
	char* file = "iris.txt";
	int numberClasses = 3;
#endif
#ifdef HOMES
	char* file = "homes.txt";
	int numberClasses = 3;
#endif
#ifdef HOUSES
	char* file = "houses.txt";
	int numberClasses = 3;
#endif

	MLFReader* reader = new MLFReader(path, file);
	int thetaCount = 0;
	int results = 0;
	std::vector<MLFData*> dataset = reader->readDataset(&thetaCount);
	delete reader;

	/*
	MLFLinear* linear = new MLFLinear(dataset, thetaCount);
	linear->solve();
	results = linear->test();
	printf("Results: %d/%d\n", results, dataset.size());
	*/

	MLFMultiLogistic* logistic = new MLFMultiLogistic(dataset, thetaCount, numberClasses);
	logistic->solve();
	results = logistic->test();

	printf("Results: %d/%d\n", results, dataset.size());

	//delete linear;
	delete logistic;
	system("pause");
}