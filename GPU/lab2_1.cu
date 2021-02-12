#include <cstdlib>
#include <iostream>
//#include <cuda_runtime.h>
#include <cuda.h>
#include <stdio.h>

#define CUDA_CHECK_RETURN(value) {\
    cudaError_t _m_cudaStat = value;\
    if (_m_cudaStat != cudaSuccess) {\
    fprintf(stderr, "Error %s at line %d in file %s\n",\
    cudaGetErrorString(_m_cudaStat), __LINE__, __FILE__);\
    exit(1);\
} }

__global__ void vecAdd_kernel(float * a, float * b, float * result, int n)
{
    int i = threadIdx.x + blockIdx.x * blockDim.x;
    a[i] = b[i] = i;
    if (i < n)
        result[i] = a[i] + b[i];
}

int main() {
    int n = 1000000;
    int threads_per_block = 8;
//    while(threads_per_block <= 1024){
        int num_of_blocks = n / threads_per_block;

        float elapsedTime;
        cudaEvent_t start,stop; // встроенный тип данных – структура, для фиксации контрольных
    //точек
        cudaEventCreate(&start); // инициализация
        cudaEventCreate(&stop); // событий

        //float * a = new float[n],
        float * a_gpu, * b_gpu, *result_gpu;
        CUDA_CHECK_RETURN(cudaMalloc((void**)&a_gpu, n * sizeof(float)));
        //float * b = new float[n], * b_gpu;
        CUDA_CHECK_RETURN(cudaMalloc((void**)&b_gpu, n * sizeof(float)));
        float * result = new float[n];//, * result_gpu;
        CUDA_CHECK_RETURN(cudaMalloc((void**)&result_gpu, n * sizeof(float)));
        //for (int i = 0; i < n; i++)
          //  a[i] = b[i] = i;
        //CUDA_CHECK_RETURN(cudaMemcpy(a_gpu, a, n * sizeof(float), cudaMemcpyHostToDevice));
        //CUDA_CHECK_RETURN(cudaMemcpy(b_gpu, b, n * sizeof(float), cudaMemcpyHostToDevice));
        cudaEventRecord(start,0); // привязка события
    //const int block_size = 256;
    //int num_blocks = (n + block_size - 1) / block_size;
    //vecAdd_kernel <<< num_blocks, block_size >>> (a_gpu, b_gpu, result_gpu, n);
        vecAdd_kernel <<< dim3(num_of_blocks), dim3(threads_per_block) >>> (a_gpu, b_gpu, result_gpu, n);
        cudaEventRecord(stop,0); // привязка события
        cudaEventSynchronize(stop); // синхронизация по событию
    //cudaDeviceSynchronize();
        CUDA_CHECK_RETURN(cudaGetLastError());
        cudaEventElapsedTime(&elapsedTime,start,stop); // вычисление затраченного времени
        fprintf(stderr,"gTest took %g \t\t num_of_blocks = %d \t\t threads_per_block = %d\n", elapsedTime, num_of_blocks, threads_per_block);
        cudaEventDestroy(start); // освобождение
        cudaEventDestroy(stop); // памяти

        CUDA_CHECK_RETURN(cudaMemcpy(result, result_gpu, n * sizeof(float), cudaMemcpyDeviceToHost));
    //for (int i = 0; i < n; i ++)
        //printf("%g\n", result[i]);
        //delete [] a;
        //delete [] b;
        delete [] result;
        cudaFree(a_gpu);
        cudaFree(b_gpu);
        cudaFree(result_gpu);
        threads_per_block *= 2;
//    }
    return 0;
}

     