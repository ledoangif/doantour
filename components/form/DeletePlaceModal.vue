<template>
    <CVModal id_model="deletePlaceModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Xóa địa điểm này</slot>
        </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa địa điểm này?</p>
            <div class="modal-footer align-content-center justify-content-center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deletePlace">
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>
<script setup>
import axios from 'axios';
import Api from '~/service/Base/api.ts';

const props = defineProps({
    PlaceId: {
        type: String,
        required: true,
    },
});

const api = new Api();
const emits = defineEmits(['Place-deleted', 'hide-modal']);
const deletePlace = () => {
    const id = props.PlaceId;
    api.deleteById('/Place', id)
        .then((res) => {
            emits('Place-deleted', id);
            emits('hide-modal');
            $('.btn-close').click();
        })
        .catch((error) => {
            console.error('Lỗi khi xóa địa điểm:', error);
        });
};
</script>

<style scoped></style>
