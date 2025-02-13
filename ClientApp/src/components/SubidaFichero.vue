<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue'

const nombre = ref('')

const onSubmit = async (e: Event) => {
  e.preventDefault()
  console.log('submit')

  const endpoint = "https://localhost:44306/api/Fichero";
  var fichero = document.getElementById("fichero") as HTMLInputElement;

  let formData = new FormData();
  fichero && fichero.files && fichero.files.length > 0 && formData.append('fichero', fichero.files[0])  
  formData.append('nombre', nombre.value)

  const response = await axios({
    method: 'post',
    url: endpoint,
    headers: { 'Content-Type': 'multipart/form-data'},
    data: formData
  });

  console.log("Respuesta", response)
    
}

const count = ref(0)
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
      <input type="file"class="form-control" id="fichero" required>
    </div>
    <button type="submit" class="btn btn-primary">Enviar</button>
  </form>
</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
