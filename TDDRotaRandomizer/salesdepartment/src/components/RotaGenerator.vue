<template>
    <div id="rota-generator">
        <b-form inline>
            <label class="mr-sm-2" for="inline-form-custom-select-pref">Generate Rota</label>
            <b-form-datepicker id="example-datepicker" class="mb-2 mr-sm-2 mb-sm-0"></b-form-datepicker>
            <b-button variant="success">Generate</b-button>
        </b-form>
    </div>
</template>

<script>

    export default {
        name: 'rota-generator',
        methods: {
            onSubmit(evt) {
                alert(JSON.stringify(this.form))
                alert(evt)
            }
        }
    }
</script>

<style scoped></style>