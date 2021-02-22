#include <cstdlib>
#include <iostream>
#include <cuda.h>
#include <stdio.h>

__global__ void gInitializeStorage(float* storage_d){
    int i = threadIdx.x + blockIdx.x * blockDim.x;
    int j = threadIdx.y + blockIdx.y * blockDim.y;
    int N = blockDim.x * gridDim.x;
    storage_d[i + j * N] = (float)(i + j * N);
}

__global__ void gTranspose0(float* storage_d, float* storage_d_t){
    int i = threadIdx.x + blockIdx.x * blockDim.x;
    int j = threadIdx.y + blockIdx.y * blockDim.y;
    int N = blockDim.x * gridDim.x;
    storage_d_t[j + i * N] = storage_d[i + j * N];
}

void Output(float* a, int N){
     for(int i = 0; i < N; i++){
        for(int j = 0; j < N; j++)
            fprintf(stdout, "%g\t", a[j + i * N]);
            fprintf(stdout, "\n");
    }
    fprintf(stdout,"\n\n\n");
}

int main() {
    int N = 256;
    int dim_of_threads = 32;
    int dim_of_blocks = N / dim_of_threads; 

    float *storage_d, *storage_d_t, *storage_h;

    cudaMalloc((void**)&storage_d, N*N*sizeof(float));
    cudaMalloc((void**)&storage_d_t, N*N*sizeof(float));
    storage_h=(float*)calloc(N*N, sizeof(float));

    gInitializeStorage <<< dim3(dim_of_blocks, dim_of_blocks), dim3(dim_of_threads, dim_of_threads) >>> (storage_d);
    
    cudaDeviceSynchronize();
    memset(storage_h, 0.0, N * N * sizeof(float));
    cudaMemcpy(storage_h, storage_d, N * N * sizeof(float), cudaMemcpyDeviceToHost);
    Output(storage_h, N);

    gTranspose0 <<< dim3(dim_of_blocks, dim_of_blocks), dim3(dim_of_threads, dim_of_threads) >>> (storage_d, storage_d_t);
    cudaDeviceSynchronize();
    memset(storage_h, 0.0, N * N * sizeof(float));
    cudaMemcpy(storage_h, storage_d_t, N * N * sizeof(float), cudaMemcpyDeviceToHost);
    Output(storage_h, N);

    cudaFree(storage_d);
    cudaFree(storage_d_t);
    free(storage_h);
        
    return 0;
}