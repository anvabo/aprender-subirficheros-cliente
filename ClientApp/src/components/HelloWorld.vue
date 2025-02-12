<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue'

defineProps<{ msg: string }>()

const nombre = ref('')

const onSubmit = async (e: Event) => {
  e.preventDefault()
  console.log('submit')

  const endpoint = "https://localhost:44306/api/xxx/xxxxx";
  var fichero = document.getElementById("fichero") as HTMLInputElement;

  let formData = new FormData();
  fichero && fichero.files && fichero.files.length > 0 && formData.append('archivo', fichero.files[0])  
  formData.append('nombre', nombre.value)

  const response = await axios({
    method: 'post',
    url: endpoint,
    headers: { 'Content-Type': 'multipart/form-data'},
    data: formData
  });

    
}

const count = ref(0)
</script>

<template>
  <h1>{{ msg }}</h1>

  <div class="card">
    <button class="btn btn-primary" type="button" @click="count++">count is {{ count }}</button>
    <p>
      Edit
      <code>components/HelloWorld.vue</code> to test HMR
    </p>
  </div>

  <form @submit.prevent="onSubmit" method="post" enctype="multipart/form-data">
    <div class="mb-3">
      <label for="name" class="form-label">Nombre</label>
      <input type="text" v-model="nombre" class="form-control" id="name" aria-describedby="nameHelp" required>
      <div id="nameHelp" class="form-text">Por favor, introduce tu nombre.</div>
    </div>
    <div class="mb-3">
      <label for="file" class="form-label">Subir fichero</label>
      <input type="file"class="form-control" id="file" required>
    </div>
    <button type="submit" class="btn btn-primary">Enviar</button>
  </form>
</template>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
