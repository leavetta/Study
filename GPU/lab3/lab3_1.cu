#include <cstdlib>
#include <iostream>
#include <cuda.h>
#include <stdio.h>

#define CUDA_CHECK_RETURN(value) {\
    cudaError_t _m_cudaStat = value;\
    if (_m_cudaStat != cudaSuccess) {\
    fprintf(stderr, "Error %s at line %d in file %s\n",\
    cudaGetErrorString(_m_cudaStat), __LINE__, __FILE__);\
    exit(1);\
} }


__global__ void gTest1(float* a){
    int i = threadIdx.x + blockIdx.x * blockDim.x;
    int j = threadIdx.y + blockIdx.y * blockDim.y;
    int I = gridDim.x * blockDim.x;
    //int J = gridDim.y * blockDim.y;
    a[i + j * I] = (float)(threadIdx.x + blockDim.y * blockIdx.x);
}

__global__ void gTest2(float* a){
    int i = threadIdx.x + blockIdx.x * blockDim.x;
    int j = threadIdx.y + blockIdx.y * blockDim.y;
    //int I = gridDim.x * blockDim.x;
    int J = gridDim.y * blockDim.y;
    a[j + i * J] = (float)(threadIdx.y + threadIdx.x * blockDim.y);
}


int main() {
    int n = 256;
    int threads_per_block = 32;
    int num_of_blocks = n / threads_per_block;

    float elapsedTime;
    cudaEvent_t start,stop;
    cudaEventCreate(&start);
    cudaEventCreate(&stop);
    float * a_gpu, * b_gpu, *result_a, *result_b;
    CUDA_CHECK_RETURN(cudaMalloc((void**)&a_gpu, n * n * sizeof(float)));
    CUDA_CHECK_RETURN(cudaMalloc((void**)&b_gpu, n * n * sizeof(float)));
    result_a = (float*)calloc(n * n, sizeof(float));
    result_b = (float*)calloc(n * n, sizeof(float));

    cudaEventRecord(start,0);

    gTest1 <<< dim3(num_of_blocks), dim3(threads_per_block) >>> (a_gpu);
    cudaEventRecord(stop,0);
    cudaEventSynchronize(stop);
    CUDA_CHECK_RETURN(cudaGetLastError());
    cudaEventElapsedTime(&elapsedTime,start,stop);
    fprintf(stderr,"gTest1 took %g \t\t num_of_blocks = %d \t\t threads_per_block = %d\n", elapsedTime, num_of_blocks, threads_per_block);
    cudaEventDestroy(start);
    cudaEventDestroy(stop);

    CUDA_CHECK_RETURN(cudaMemcpy(result_a, a_gpu, n * n * sizeof(float), cudaMemcpyDeviceToHost));

    cudaEventCreate(&start);
    cudaEventCreate(&stop);
    cudaEventRecord(start,0);
    gTest2 <<< dim3(num_of_blocks), dim3(threads_per_block) >>> (b_gpu);
    cudaEventRecord(stop,0);
    cudaEventSynchronize(stop);
    CUDA_CHECK_RETURN(cudaGetLastError());
    cudaEventElapsedTime(&elapsedTime,start,stop);
    fprintf(stderr,"gTest2 took %g \t\t num_of_blocks = %d \t\t threads_per_block = %d\n", elapsedTime, num_of_blocks, threads_per_block);
    cudaEventDestroy(start);
    cudaEventDestroy(stop);
    CUDA_CHECK_RETURN(cudaMemcpy(result_b, b_gpu, n * n * sizeof(float), cudaMemcpyDeviceToHost));

    cudaFree(a_gpu);
    cudaFree(b_gpu);
    free(result_a);
    free(result_b);

    return 0;
}

