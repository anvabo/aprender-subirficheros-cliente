<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue'

const nombre = ref('')
const files = ref<File[]>([]);

const onSubmit = async (e: Event) => {
  e.preventDefault()
  console.log('submit')

  const endpoint = "https://localhost:44306/api/Fichero/FicheroMultiple";
  
  let formData = new FormData();
  files.value.forEach((file) => {
    if (file) {
      formData.append('ficheros', file);
    }
  });

  formData.append('nombre', nombre.value)

  const response = await axios({
    method: 'post',
    url: endpoint,
    headers: { 'Content-Type': 'multipart/form-data'},
    data: formData
  }).then((response) => {
    console.log("Respuesta", response)
    document.getElementById("alert")!.classList.remove("d-none");
    document.getElementById("alert")!.classList.add("d-block");
    document.getElementById("alert")!.querySelector("p")!.innerHTML = "Fichero subido correctamente: <a href='" + response.data + "'>Descargar</a>";
    document.getElementById("img")!.setAttribute("src", URL.createObjectURL(fichero.files[0]));
  }).catch((error) => {
    console.log("Error", error)
    document.getElementById("alert")!.classList.remove("d-none");
    document.getElementById("alert")!.classList.add("d-block");
    document.getElementById("alert")!.querySelector("p")!.innerText = "Error al subir el fichero";
  });    
}

  const addFile = () => {
    files.value.push(null as any);
  };

  const removeFile = (index: number) => {
    files.value.splice(index, 1);
  };

  const handleFileChange = (event: Event, index: number) => {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      files.value[index] = target.files[0];
    }
  };
</script>

<template>

  <form @submit.prevent="onSubmit" method="post" enctype="multipart/form-data">
    <div class="mb-3">
      <label for="name" class="form-label">Nombre</label>
      <input type="text" v-model="nombre" class="form-control" id="name" aria-describedby="nameHelp" required>
      <div id="nameHelp" class="form-text">Por favor, introduce tu nombre.</div>
    </div>
    <div class="mb-3">
      <label for="file" class="form-label">Subir fichero</label>
      <div v-for="(file, index) in files" :key="index" class="mb-2 input-group">
        <input type="file" class="form-control" :id="'fichero' + index" @change="handleFileChange($event, index)" required>
        <button type="button" class="btn btn-danger" @click="removeFile(index)">- Eliminar fichero</button>
      </div>
      <div class="row">
        <button type="button" class="btn btn-primary" @click="addFile">+ Nuevo fichero</button>
      </div>
    </div>
    <div id="alert" class="alert alert-success mt-3 d-none">
            <p></p>
            <img id="img" width="200px" />
        </div>
    <button type="submit" class="btn btn-primary">Enviar</button>
  </form>
</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
