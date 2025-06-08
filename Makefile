APP = auditoria

## All Environment
setup-dev: setup-kind deploy-dev

# 1 - Create cluster only
setup-kind:
	@kind create cluster --config kubernetes/kind/config.yaml --wait 60s
	@kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
	@kubectl patch deployment metrics-server -n kube-system --type='json' -p='[{"op": "add", "path": "/spec/template/spec/containers/0/args/-", "value": "--kubelet-insecure-tls"}]'
	@kubectl apply -f https://github.com/cert-manager/cert-manager/releases/download/v1.15.1/cert-manager.yaml
	@kubectl wait --namespace cert-manager \
		--for=condition=ready pod \
		--selector=app.kubernetes.io/instance=cert-manager \
		--timeout=270s
	@kubectl apply -f https://github.com/open-telemetry/opentelemetry-operator/releases/latest/download/opentelemetry-operator.yaml
	@helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
	@helm repo update
	@helm upgrade --install \
		--version 4.11.0 \
		--namespace ingress-nginx \
		--set installCRDs=true \
		--set controller.service.externalTrafficPolicy="Local" \
		--set controller.service.ipFamilyPolicy="PreferDualStack" \
		--set controller.service.ipFamilies[0]="IPv4" \
		--create-namespace \
		ingress-nginx ingress-nginx/ingress-nginx
	@kubectl wait --namespace opentelemetry-operator-system \
		--for=condition=ready pod \
		--selector=app.kubernetes.io/name=opentelemetry-operator \
		--timeout=270s
	@kubectl apply -f kubernetes/open-telemetry/
	@helm upgrade --install \
		prometheus prometheus-community/prometheus \
		--version 25.25.0 \
		-n monitoring \
		--create-namespace \
		-f kubernetes/prometheus-values.yaml
	@kubectl wait --namespace monitoring \
		--for=condition=ready pod \
		--selector=app.kubernetes.io/name=prometheus \
		--timeout=270s
	@helm upgrade --install \
		lgtm \
		-n monitoring \
		--create-namespace \
		grafana/lgtm-distributed \
		--version 2.1.0 \
		-f kubernetes/lgtm/values-lgtm-distributed.yaml
	@kubectl wait --namespace monitoring \
		--for=condition=ready pod \
		--selector=app.kubernetes.io/instance=lgtm \
		--timeout=270s

deploy-dev:
	@docker buildx build -t $(APP):latest -f Dockerfile .
	@kind load docker-image --name auditoria-cluster-kind $(APP):latest
	@kubectl apply -f kubernetes/manifests/
	@kubectl rollout restart deploy auditoria

teardown-dev:
	@kind delete clusters auditoria-cluster-kind