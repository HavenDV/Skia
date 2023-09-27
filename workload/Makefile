TOP := $(dir $(abspath $(lastword $(MAKEFILE_LIST))))

TMPDIR := $(TOP)/.tmp
OUTDIR := $(TOP)/out
WORKLOAD_PACKS_OUTDIR := $(OUTDIR)/nuget-unsigned
WORKLOAD_MSI_OUTDIR := $(OUTDIR)/windows

.DEFAULT_GOAL := packs

include $(TOP)/Config.mk

DIRECTORIES += \
	$(OUTDIR) \
	$(TMPDIR) \
	$(DOTNET_MANIFESTS_DESTDIR)

$(DIRECTORIES):
	@mkdir -p $@

# Install dotnet sdk for internal use
DOTNET := $(DOTNET_DESTDIR)/dotnet

$(DOTNET): | $(TMPDIR)/dotnet-install.sh
	@bash $(TMPDIR)/dotnet-install.sh -v $(DOTNET_VERSION) -i $(DOTNET_DESTDIR)

$(TMPDIR)/dotnet-install.sh: | $(OUTDIR)
	@curl -o $@ \
		https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh


# Create nuget packages for manifest and packs
define CreateNuGetPkgs
$(WORKLOAD_PACKS_OUTDIR)/$(1)$(3).$(2).nupkg: $(DOTNET)
		@$(DOTNET) pack --nologo $(TOP)/build/$(1).proj \
			-p:Configuration=Release \
			-p:IncludeSymbols=False \
			-p:SkiaPackVersion=$(2) \
			-p:SkiaVersionHash=$(CURRENT_HASH) \
			-p:DotNetPreviewVersionBand=$(4)

NUPKG_TARGETS += $(WORKLOAD_PACKS_OUTDIR)/$(1)$(3).$(2).nupkg
endef

$(eval $(call CreateNuGetPkgs,NET.Sdk.Skia,$(SKIA_WORKLOAD_VERSION_FULL),.Manifest-$(DOTNET_VERSION_BAND),$(DOTNET_VERSION_BAND)))
$(eval $(call CreateNuGetPkgs,Skia.Sdk,$(SKIA_WORKLOAD_VERSION_FULL)))
$(eval $(call CreateNuGetPkgs,Skia.Ref,$(SKIA_WORKLOAD_VERSION_FULL)))
$(eval $(call CreateNuGetPkgs,Skia.Templates,$(SKIA_WORKLOAD_VERSION_FULL)))
$(eval $(call CreateNuGetPkgs,Skia.NETCore.App.Runtime,$(SKIA_WORKLOAD_VERSION_FULL),.skia))

.PHONY: packs
packs: $(NUPKG_TARGETS)

# Install workload to the dotnet sdk
$(TMPDIR)/.stamp-install-workload: | $(DOTNET_MANIFESTS_DESTDIR)
	@cp -f \
		$(TOP)/LICENSE \
		$(TOP)/src/NET.Sdk.Skia/WorkloadManifest.targets \
		$(WORKLOAD_PACKS_OUTDIR)/workload-manifest/WorkloadManifest.json \
		$(DOTNET_MANIFESTS_DESTDIR)
	@$(DOTNET) workload install skia --skip-manifest-update \
		--source $(WORKLOAD_PACKS_OUTDIR) --temp-dir=$(TMPDIR)
	@touch $@

.PHONY: install
install: packs $(TMPDIR)/.stamp-install-workload


# Uninstall workload from the dotnet sdk
.PHONY: uninstall
uninstall:
	@$(DOTNET) workload uninstall skia
	@rm -f $(TMPDIR)/.stamp-install-workload

# Create MSI windows installer
define CreateMsi
$(WORKLOAD_MSI_OUTDIR)/NET.Workload.Skia.$(1).wix: | $(TMPDIR)/msi
	@$(DOTNET) msbuild --nologo $(TOP)/build/GenerateWixFile.proj \
									-t:Generate \
									-p:MSIVersion=$(SKIA_WORKLOAD_VERSION_FULL) \
									-p:SourceDirectory=$(TMPDIR)/msi \
									-p:DestinationFile="$$@"

$(WORKLOAD_MSI_OUTDIR)/NET.Workload.Skia.$(1).msi: $(WORKLOAD_MSI_OUTDIR)/NET.Workload.Skia.$(1).wix
	@wixl -o "$$@" "$$<" -a x64 -v

MSI_TARGET := $(WORKLOAD_MSI_OUTDIR)/NET.Workload.Skia.$(1).msi
endef

$(TMPDIR)/msi: install
	@mkdir -p $@/sdk-manifests/$(DOTNET_VERSION_BAND)
	@cp -fr $(DOTNET_MANIFESTS_DESTDIR) $@/sdk-manifests/$(DOTNET_VERSION_BAND)
	@mkdir -p $@/packs
	@cp -fr $(DOTNET_DESTDIR)/packs/Skia.Sdk $@/packs
	@cp -fr $(DOTNET_DESTDIR)/packs/Skia.Ref $@/packs
	@cp -fr $(DOTNET_DESTDIR)/packs/Skia.NETCore.App.Runtime.* $@/packs
	@mkdir -p $@/template-packs
	@cp -f $(DOTNET_DESTDIR)/template-packs/skia.templates.*.nupkg $@/template-packs

$(eval $(call CreateMsi,$(SKIA_WORKLOAD_VERSION_FULL)))

msi: $(MSI_TARGET)
	@rm -fr $(TMPDIR)

# Test 'skia' workload
.PHONY: test
test: install
	@rm -fr $(TMPDIR)/test
	@mkdir -p $(TMPDIR)/test
	@$(DOTNET) new skia --output $(TMPDIR)/test
	@$(DOTNET) build $(TMPDIR)/test


# Remove artifacts and temporary files
clean:
	@rm -fr $(OUTDIR)
	@rm -fr $(TMPDIR)
	@rm -fr $(TOP)/build/obj/
